using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Reportes._2.Database;
using Reportes._1.Logic;

namespace Reportes._4.UI
{
    public partial class Prestamos : Form
    {
        Conexion cn = new Conexion();
        PrestamoLogic logic = new PrestamoLogic();

        public Prestamos()
        {
            InitializeComponent();
            CargarClientes();
            CargarPrestamos();
        }

        private void CargarClientes()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT ClienteId, NombreCompleto FROM Clientes", cn.ObtenerConexion());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbClientes.DataSource = dt;
            cmbClientes.DisplayMember = "NombreCompleto";
            cmbClientes.ValueMember = "ClienteId";
        }

        private void CargarPrestamos()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Prestamos", cn.ObtenerConexion());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void cmbClientes_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtMonto_TextChanged(object sender, EventArgs e) { }
        private void txtMeses_TextChanged(object sender, EventArgs e) { }
        private void txtInteres_TextChanged(object sender, EventArgs e) { }
        private void txtMontototal_TextChanged(object sender, EventArgs e) { }
        private void txtCuotasmensual_TextChanged(object sender, EventArgs e) { }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMonto.Text) || string.IsNullOrWhiteSpace(txtMeses.Text))
            {
                MessageBox.Show("Por favor ingrese el monto y los meses.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal monto = decimal.Parse(txtMonto.Text);
            int plazoMeses = int.Parse(txtMeses.Text);

            // Calcular tasa según el plazo automáticamente
            decimal tasa = logic.ObtenerTasaInteres(plazoMeses);
            decimal cuota = logic.CalcularCuotaMensual(monto, plazoMeses, tasa);
            decimal interes = logic.CalcularInteresTotal(cuota, plazoMeses, monto);
            decimal montoTotal = monto + interes;

            // Llenar campos automáticamente
            txtInteres.Text = interes.ToString("F2");
            txtMontototal.Text = montoTotal.ToString("F2");
            txtCuotasmensual.Text = cuota.ToString("F2");
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMonto.Text) ||
                string.IsNullOrWhiteSpace(txtMeses.Text) ||
                string.IsNullOrWhiteSpace(txtInteres.Text))
            {
                MessageBox.Show("Primero calcule el préstamo antes de registrar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int clienteId = (int)cmbClientes.SelectedValue;
            decimal monto = decimal.Parse(txtMonto.Text);
            int plazoMeses = int.Parse(txtMeses.Text);
            decimal tasa = logic.ObtenerTasaInteres(plazoMeses);
            decimal cuota = decimal.Parse(txtCuotasmensual.Text);
            decimal interes = decimal.Parse(txtInteres.Text);
            decimal montoTotal = decimal.Parse(txtMontototal.Text);

            // Validar fondo disponible
            SqlConnection conn = cn.ObtenerConexion();
            SqlCommand cmdFondo = new SqlCommand("SELECT CapitalDisponible FROM FondoEntidad", conn);
            decimal fondoDisponible = (decimal)cmdFondo.ExecuteScalar();

            if (!logic.ValidarFondoDisponible(fondoDisponible, monto))
            {
                MessageBox.Show("Fondo insuficiente para este préstamo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }

            // Insertar préstamo
            string query = "INSERT INTO Prestamos (ClienteId, Monto, PlazoMeses, TasaInteres, Interes, MontoTotal, CuotaMensual) " +
                           "VALUES (@ClienteId, @Monto, @Plazo, @Tasa, @Interes, @MontoTotal, @Cuota)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ClienteId", clienteId);
            cmd.Parameters.AddWithValue("@Monto", monto);
            cmd.Parameters.AddWithValue("@Plazo", plazoMeses);
            cmd.Parameters.AddWithValue("@Tasa", tasa);
            cmd.Parameters.AddWithValue("@Interes", interes);
            cmd.Parameters.AddWithValue("@MontoTotal", montoTotal);
            cmd.Parameters.AddWithValue("@Cuota", cuota);
            cmd.ExecuteNonQuery();

            // Obtener el PrestamoId recién insertado
            SqlCommand cmdId = new SqlCommand("SELECT MAX(PrestamoId) FROM Prestamos", conn);
            int prestamoId = (int)cmdId.ExecuteScalar();

            // Generar tabla de amortización
            var tablaAmortizacion = logic.GenerarTablaAmortizacion(prestamoId, monto, plazoMeses, tasa);
            foreach (var fila in tablaAmortizacion)
            {
                SqlCommand cmdAmort = new SqlCommand(
                    "INSERT INTO TablaAmortizacion (PrestamoId, NumeroCuota, Cuota, InteresDelMes, CapitalAmortizado, SaldoRestante, Pagado) " +
                    "VALUES (@PrestamoId, @NumeroCuota, @Cuota, @Interes, @Capital, @Saldo, @Pagado)", conn);
                cmdAmort.Parameters.AddWithValue("@PrestamoId", fila.PrestamoId);
                cmdAmort.Parameters.AddWithValue("@NumeroCuota", fila.NumeroCuota);
                cmdAmort.Parameters.AddWithValue("@Cuota", fila.Cuota);
                cmdAmort.Parameters.AddWithValue("@Interes", fila.InteresDelMes);
                cmdAmort.Parameters.AddWithValue("@Capital", fila.CapitalAmortizado);
                cmdAmort.Parameters.AddWithValue("@Saldo", fila.SaldoRestante);
                cmdAmort.Parameters.AddWithValue("@Pagado", fila.Pagado);
                cmdAmort.ExecuteNonQuery();
            }

            // Actualizar fondo
            SqlCommand cmdUpdateFondo = new SqlCommand(
                "UPDATE FondoEntidad SET CapitalDisponible = CapitalDisponible - @Monto, TotalPrestado = TotalPrestado + @Monto", conn);
            cmdUpdateFondo.Parameters.AddWithValue("@Monto", monto);
            cmdUpdateFondo.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Préstamo registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar campos
            txtMonto.Clear();
            txtMeses.Clear();
            txtInteres.Clear();
            txtMontototal.Clear();
            txtCuotasmensual.Clear();

            CargarPrestamos();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}