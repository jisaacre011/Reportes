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
    public partial class Moras : Form
    {
        Conexion cn = new Conexion();
        PrestamoLogic logic = new PrestamoLogic();
        private int prestamoIdActual = 0;

        public Moras()
        {
            InitializeComponent();
            CargarClientes();
            CargarMoras();
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

            // ✅ Orden correcto
            cmbClientes.DisplayMember = "NombreCompleto";
            cmbClientes.ValueMember = "ClienteId";
            cmbClientes.DataSource = dt;
            cmbClientes.SelectedIndex = 0;
        }

        private void CargarMoras()
        {
            SqlConnection conn = cn.ObtenerConexion();
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT P.PrestamoId, C.NombreCompleto, P.CuotaMensual, P.CantidadMoras, P.Estado " +
                "FROM Prestamos P INNER JOIN Clientes C ON P.ClienteId = C.ClienteId " +
                "WHERE P.CantidadMoras > 0", conn);
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

            txtCuotasmensual.Clear();
            txtCantidadMora.Clear();
            prestamoIdActual = 0;

            SqlConnection conn = cn.ObtenerConexion();
            SqlCommand cmd = new SqlCommand(
                "SELECT TOP 1 PrestamoId, CuotaMensual, CantidadMoras " +
                "FROM Prestamos WHERE ClienteId = @ClienteId AND Estado = 'Activo'", conn);
            cmd.Parameters.AddWithValue("@ClienteId", clienteId);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                prestamoIdActual = (int)reader["PrestamoId"];
                txtCuotasmensual.Text = reader["CuotaMensual"].ToString();
                txtCantidadMora.Text = reader["CantidadMoras"].ToString();
            }
            else
            {
                MessageBox.Show("Este cliente no tiene préstamos activos.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            reader.Close();
            conn.Close();
        }

        private void txtCuotasmensual_TextChanged(object sender, EventArgs e) { }
        private void txtCantidadMora_TextChanged(object sender, EventArgs e) { }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCuotasmensual.Text))
            {
                MessageBox.Show("Seleccione un cliente con préstamo activo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal cuota = decimal.Parse(txtCuotasmensual.Text);
            int cantidadMoras = int.Parse(txtCantidadMora.Text);
            decimal mora = logic.CalcularMora(cuota);
            bool esMoroso = logic.EsMoroso(cantidadMoras);

            string mensaje = $"Mora por cuota: {mora:C}\n";
            mensaje += esMoroso
                ? "⚠️ Cliente marcado como MOROSO (3 o más moras)"
                : $"Moras acumuladas: {cantidadMoras} (necesita {3 - cantidadMoras} más para ser moroso)";

            MessageBox.Show(mensaje, "Cálculo de Mora", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (prestamoIdActual == 0)
            {
                MessageBox.Show("Seleccione un cliente con préstamo activo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlConnection conn = cn.ObtenerConexion();

            SqlCommand cmdMora = new SqlCommand(
                "UPDATE Prestamos SET CantidadMoras = CantidadMoras + 1 WHERE PrestamoId = @PrestamoId", conn);
            cmdMora.Parameters.AddWithValue("@PrestamoId", prestamoIdActual);
            cmdMora.ExecuteNonQuery();

            SqlCommand cmdCheck = new SqlCommand(
                "SELECT CantidadMoras FROM Prestamos WHERE PrestamoId = @PrestamoId", conn);
            cmdCheck.Parameters.AddWithValue("@PrestamoId", prestamoIdActual);
            int cantidadMoras = (int)cmdCheck.ExecuteScalar();

            if (logic.EsMoroso(cantidadMoras))
            {
                SqlCommand cmdEstado = new SqlCommand(
                    "UPDATE Prestamos SET Estado = 'Moroso' WHERE PrestamoId = @PrestamoId", conn);
                cmdEstado.Parameters.AddWithValue("@PrestamoId", prestamoIdActual);
                cmdEstado.ExecuteNonQuery();

                SqlCommand cmdCliente = new SqlCommand(
                    "UPDATE Clientes SET EsMoroso = 1 WHERE ClienteId = (SELECT ClienteId FROM Prestamos WHERE PrestamoId = @PrestamoId)", conn);
                cmdCliente.Parameters.AddWithValue("@PrestamoId", prestamoIdActual);
                cmdCliente.ExecuteNonQuery();

                MessageBox.Show("Mora registrada. ⚠️ Cliente marcado como MOROSO.", "Moroso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Mora registrada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            conn.Close();

            txtCuotasmensual.Clear();
            txtCantidadMora.Clear();
            prestamoIdActual = 0;
            cmbClientes.SelectedIndex = 0;

            CargarMoras();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}