using System;
using System.Collections.Generic;
using Reportes.Entities;

namespace Reportes.BusinessLogic
{
    public class PrestamoLogic
    {
        // =============================================
        // VALIDACIONES
        // =============================================

        // Valida que el préstamo no supere 4 veces el sueldo
        public bool ValidarMontoMaximo(decimal sueldo, decimal montoPrestamo)
        {
            return montoPrestamo <= sueldo * 4;
        }

        // Valida que el cliente tenga garantía registrada
        public bool ValidarGarantia(string garantia)
        {
            return !string.IsNullOrWhiteSpace(garantia);
        }

        // Valida que el fondo base tenga suficiente dinero
        public bool ValidarFondoDisponible(decimal fondoDisponible, decimal montoPrestamo)
        {
            return fondoDisponible >= montoPrestamo;
        }

        // =============================================
        // CÁLCULO DE TASA DE INTERÉS SEGÚN EL PLAZO
        // =============================================
        // 1–3 meses  → 10%
        // 4–6 meses  →  8%
        // 7–12 meses →  7%
        // +12 meses  →  5%

        public decimal ObtenerTasaInteres(int plazoMeses)
        {
            if (plazoMeses >= 1 && plazoMeses <= 3)
                return 10m;
            else if (plazoMeses >= 4 && plazoMeses <= 6)
                return 8m;
            else if (plazoMeses >= 7 && plazoMeses <= 12)
                return 7m;
            else
                return 5m;
        }

        // =============================================
        // CÁLCULO DE CUOTA MENSUAL (FÓRMULA FRANCESA)
        // =============================================
        // Fórmula: C = P * (i * (1+i)^n) / ((1+i)^n - 1)
        // P = capital, i = tasa mensual, n = meses

        public decimal CalcularCuotaMensual(decimal monto, int plazoMeses, decimal tasaAnual)
        {
            decimal tasaMensual = (tasaAnual / 100m) / 12m;

            if (tasaMensual == 0)
                return monto / plazoMeses;

            double i = (double)tasaMensual;
            double potencia = Math.Pow(1 + i, plazoMeses);
            double cuota = (double)monto * (i * potencia) / (potencia - 1);

            return Math.Round((decimal)cuota, 2);
        }

        // =============================================
        // CÁLCULO DEL INTERÉS TOTAL
        // =============================================

        public decimal CalcularInteresTotal(decimal cuotaMensual, int plazoMeses, decimal monto)
        {
            return Math.Round((cuotaMensual * plazoMeses) - monto, 2);
        }

        // =============================================
        // TABLA DE AMORTIZACIÓN COMPLETA
        // =============================================

        public List<TablaAmortizacion> GenerarTablaAmortizacion(int prestamoId, decimal monto, int plazoMeses, decimal tasaAnual)
        {
            List<TablaAmortizacion> tabla = new List<TablaAmortizacion>();

            decimal tasaMensual = (tasaAnual / 100m) / 12m;
            decimal cuota = CalcularCuotaMensual(monto, plazoMeses, tasaAnual);
            decimal saldo = monto;

            for (int mes = 1; mes <= plazoMeses; mes++)
            {
                decimal interesMes = Math.Round(saldo * tasaMensual, 2);
                decimal capitalMes = Math.Round(cuota - interesMes, 2);

                // Ajuste en el último mes para evitar diferencias por redondeo
                if (mes == plazoMeses)
                    capitalMes = saldo;

                decimal nuevoSaldo = Math.Round(saldo - capitalMes, 2);

                tabla.Add(new TablaAmortizacion
                {
                    PrestamoId = prestamoId,
                    NumeroCuota = mes,
                    Cuota = cuota,
                    InteresDelMes = interesMes,
                    CapitalAmortizado = capitalMes,
                    SaldoRestante = nuevoSaldo,
                    Pagado = false
                });

                saldo = nuevoSaldo;
            }

            return tabla;
        }

        // =============================================
        // CÁLCULO DE MORA (10% de la cuota)
        // =============================================

        public decimal CalcularMora(decimal cuotaMensual)
        {
            return Math.Round(cuotaMensual * 0.10m, 2);
        }

        // =============================================
        // MARCAR CLIENTE COMO MOROSO (3 moras acumuladas)
        // =============================================

        public bool EsMoroso(int cantidadMoras)
        {
            return cantidadMoras >= 3;
        }

        // =============================================
        // RECALCULAR INTERÉS CON ABONO EXTRAORDINARIO
        // =============================================

        public List<TablaAmortizacion> RecalcularConAbono(int prestamoId, decimal saldoActual, int mesesRestantes, decimal tasaAnual)
        {
            return GenerarTablaAmortizacion(prestamoId, saldoActual, mesesRestantes, tasaAnual);
        }
    }
}