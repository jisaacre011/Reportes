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
    public partial class Pagos : Form
    {
        Conexion cn = new Conexion();
        PrestamoLogic logic = new PrestamoLogic();
        private string txtTasaPrestamo = "";
        private int prestamoIdActual = 0;

        public Pagos()
        {
            InitializeComponent();
            CargarClientes();
            CargarPagos();
        }

        private void CargarClientes()
        {
            SqlConnection conn = cn.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter("SELECT ClienteId, NombreCompleto FROM Clientes", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();

            DataRow fila = dt.NewRow();
            fila["ClienteId"] = 0;
            fila["NombreCompleto"] = "-- Seleccione un cliente --";
            dt.Rows.InsertAt(fila, 0);

            cmbClientes.DisplayMember = "NombreCompleto";
            cmbClientes.ValueMember = "ClienteId";
            cmbClientes.DataSource = dt;
            cmbClientes.SelectedIndex = 0;
        }

        private void CargarPagos()
        {
            SqlConnection conn = cn.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Pagos", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void cmbClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbClientes.SelectedValue == null) return;
            int clienteId = Convert.ToInt32(cmbClientes.SelectedValue);
            if (clienteId == 0) return;

            txtMontoanterior.Clear();
            txtCuotas.Clear();
            txtInteres.Clear();
            txtNuevomonto.Clear();
            txtTotalintereses.Clear();
            txtTasaPrestamo = "";
            prestamoIdActual = 0;

            SqlConnection conn = cn.ObtenerConexion();
            SqlCommand cmd = new SqlCommand(
                "SELECT TOP 1 PrestamoId, Monto, CuotaMensual, TasaInteres, PlazoMeses " +
                "FROM Prestamos WHERE ClienteId = @ClienteId AND Estado = 'Activo'", conn);
            cmd.Parameters.AddWithValue("@ClienteId", clienteId);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                prestamoIdActual = (int)reader["PrestamoId"];
                txtMontoanterior.Text = reader["Monto"].ToString();
                txtCuotas.Text = reader["CuotaMensual"].ToString();
                txtTasaPrestamo = reader["TasaInteres"].ToString();
            }
            else
            {
                MessageBox.Show("Este cliente no tiene préstamos activos.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reader.Close();
            conn.Close();
        }

        private void txtMontoanterior_TextChanged(object sender, EventArgs e) { }
        private void txtCuotas_TextChanged(object sender, EventArgs e) { }
        private void txtInteres_TextChanged(object sender, EventArgs e) { }
        private void txtNuevomonto_TextChanged(object sender, EventArgs e) { }
        private void txtMesesrestantes_TextChanged(object sender, EventArgs e) { }
        private void txtTotalintereses_TextChanged(object sender, EventArgs e) { }

        private void btnCalcularpago_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMontoanterior.Text) ||
                string.IsNullOrWhiteSpace(txtCuotas.Text) ||
                string.IsNullOrWhiteSpace(txtMesesrestantes.Text) ||
                string.IsNullOrWhiteSpace(txtTasaPrestamo))
            {
                MessageBox.Show("Seleccione un cliente con préstamo activo e ingrese los meses restantes.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal montoAnterior = decimal.Parse(txtMontoanterior.Text);
            decimal cuota = decimal.Parse(txtCuotas.Text);
            int mesesRestantes = int.Parse(txtMesesrestantes.Text);
            decimal tasa = decimal.Parse(txtTasaPrestamo);

            decimal tasaMensual = (tasa / 100m) / 12m;
            decimal interesMes = Math.Round(montoAnterior * tasaMensual, 2);
            decimal capital = Math.Round(cuota - interesMes, 2);
            decimal nuevoMonto = Math.Round(montoAnterior - capital, 2);
            decimal totalIntereses = logic.CalcularInteresTotal(cuota, mesesRestantes, montoAnterior);

            SqlConnection conn = cn.ObtenerConexion();
            SqlCommand cmdMoras = new SqlCommand(
                "SELECT CantidadMoras FROM Prestamos WHERE PrestamoId = @PrestamoId", conn);
            cmdMoras.Parameters.AddWithValue("@PrestamoId", prestamoIdActual);
            int cantidadMoras = (int)cmdMoras.ExecuteScalar();
            conn.Close();

            if (logic.EsMoroso(cantidadMoras))
            {
                decimal mora = logic.CalcularMora(cuota);
                MessageBox.Show($"Cliente moroso. Se aplica mora de: {mora:C}", "Mora", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            txtInteres.Text = interesMes.ToString("F2");
            txtNuevomonto.Text = nuevoMonto.ToString("F2");
            txtTotalintereses.Text = totalIntereses.ToString("F2");
        }

        private void btnRegistrarpago_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInteres.Text) ||
                string.IsNullOrWhiteSpace(txtNuevomonto.Text))
            {
                MessageBox.Show("Primero calcule el pago antes de registrar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener prestamoId directo desde el cliente seleccionado
            int clienteId = Convert.ToInt32(cmbClientes.SelectedValue);
            SqlConnection connTemp = cn.ObtenerConexion();
            SqlCommand cmdTemp = new SqlCommand(
                "SELECT TOP 1 PrestamoId FROM Prestamos WHERE ClienteId = @ClienteId AND Estado = 'Activo'", connTemp);
            cmdTemp.Parameters.AddWithValue("@ClienteId", clienteId);
            object result = cmdTemp.ExecuteScalar();
            connTemp.Close();

            if (result == null)
            {
                MessageBox.Show("No se encontró un préstamo activo para este cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            prestamoIdActual = (int)result;

            decimal montoAnterior = decimal.Parse(txtMontoanterior.Text);
            decimal cuota = decimal.Parse(txtCuotas.Text);
            decimal interesPagado = decimal.Parse(txtInteres.Text);
            decimal nuevoMonto = decimal.Parse(txtNuevomonto.Text);
            decimal totalIntereses = decimal.Parse(txtTotalintereses.Text);
            int mesesRestantes = int.Parse(txtMesesrestantes.Text);
            decimal tasa = string.IsNullOrWhiteSpace(txtTasaPrestamo) ? 0 : decimal.Parse(txtTasaPrestamo);

            SqlConnection conn = cn.ObtenerConexion();

            SqlCommand cmdPrestamo = new SqlCommand(
                "SELECT CantidadMoras, (SELECT COUNT(*) FROM Pagos WHERE PrestamoId = @PrestamoId) + 1 AS NumeroCuota " +
                "FROM Prestamos WHERE PrestamoId = @PrestamoId", conn);
            cmdPrestamo.Parameters.AddWithValue("@PrestamoId", prestamoIdActual);
            SqlDataReader reader = cmdPrestamo.ExecuteReader();

            int cantidadMoras = 0;
            int numeroCuota = 1;

            if (reader.Read())
            {
                cantidadMoras = (int)reader["CantidadMoras"];
                numeroCuota = (int)reader["NumeroCuota"];
            }
            reader.Close();

            decimal mora = 0;
            bool tuveMora = logic.EsMoroso(cantidadMoras);
            if (tuveMora)
                mora = logic.CalcularMora(cuota);

            SqlCommand cmdPago = new SqlCommand(
                "INSERT INTO Pagos (PrestamoId, NumeroCuota, MontoAnterior, InteresPagado, NuevoMonto, CuotaPagada, " +
                "MesesRestantes, TotalInteresAcum, TasaPrestamo, TuveMora, MontoMora) " +
                "VALUES (@PrestamoId, @NumeroCuota, @MontoAnterior, @Interes, @NuevoMonto, @Cuota, " +
                "@MesesRestantes, @TotalIntereses, @Tasa, @TuveMora, @Mora)", conn);
            cmdPago.Parameters.AddWithValue("@PrestamoId", prestamoIdActual);
            cmdPago.Parameters.AddWithValue("@NumeroCuota", numeroCuota);
            cmdPago.Parameters.AddWithValue("@MontoAnterior", montoAnterior);
            cmdPago.Parameters.AddWithValue("@Interes", interesPagado);
            cmdPago.Parameters.AddWithValue("@NuevoMonto", nuevoMonto);
            cmdPago.Parameters.AddWithValue("@Cuota", cuota + mora);
            cmdPago.Parameters.AddWithValue("@MesesRestantes", mesesRestantes - 1);
            cmdPago.Parameters.AddWithValue("@TotalIntereses", totalIntereses);
            cmdPago.Parameters.AddWithValue("@Tasa", tasa);
            cmdPago.Parameters.AddWithValue("@TuveMora", tuveMora);
            cmdPago.Parameters.AddWithValue("@Mora", mora);
            cmdPago.ExecuteNonQuery();

            SqlCommand cmdUpdate = new SqlCommand(
                "UPDATE Prestamos SET Monto = @NuevoMonto WHERE PrestamoId = @PrestamoId", conn);
            cmdUpdate.Parameters.AddWithValue("@NuevoMonto", nuevoMonto);
            cmdUpdate.Parameters.AddWithValue("@PrestamoId", prestamoIdActual);
            cmdUpdate.ExecuteNonQuery();

            if (nuevoMonto <= 0)
            {
                SqlCommand cmdEstado = new SqlCommand(
                    "UPDATE Prestamos SET Estado = 'Pagado' WHERE PrestamoId = @PrestamoId", conn);
                cmdEstado.Parameters.AddWithValue("@PrestamoId", prestamoIdActual);
                cmdEstado.ExecuteNonQuery();
            }

            conn.Close();

            MessageBox.Show("Pago registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtMontoanterior.Clear();
            txtCuotas.Clear();
            txtInteres.Clear();
            txtNuevomonto.Clear();
            txtMesesrestantes.Clear();
            txtTotalintereses.Clear();
            txtTasaPrestamo = "";
            prestamoIdActual = 0;

            CargarPagos();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}