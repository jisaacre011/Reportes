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

namespace Reportes._4.UI
{
    public partial class Clientes : Form
    {
        Conexion cn = new Conexion();

        public Clientes()
        {
            InitializeComponent();
            CargarClientes();
        }

        private void CargarClientes()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Clientes", cn.ObtenerConexion());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void txtNombrecompleto_TextChanged(object sender, EventArgs e) { }
        private void txtCorreo_TextChanged(object sender, EventArgs e) { }
        private void txtTelefono_TextChanged(object sender, EventArgs e) { }
        private void txtDireccion_TextChanged(object sender, EventArgs e) { }
        private void txtGarantia_TextChanged(object sender, EventArgs e) { }
        private void txtSueldo_TextChanged(object sender, EventArgs e) { }
        private void chkMoroso_CheckedChanged(object sender, EventArgs e) { }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(txtNombrecompleto.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtGarantia.Text) ||
                string.IsNullOrWhiteSpace(txtSueldo.Text))
            {
                MessageBox.Show("Por favor complete todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlConnection conn = cn.ObtenerConexion();
            string query = "INSERT INTO Clientes (NombreCompleto, Correo, Telefono, Direccion, Garantia, Sueldo, EsMoroso) " +
                           "VALUES (@Nombre, @Correo, @Telefono, @Direccion, @Garantia, @Sueldo, @EsMoroso)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nombre", txtNombrecompleto.Text);
            cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
            cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
            cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
            cmd.Parameters.AddWithValue("@Garantia", txtGarantia.Text);
            cmd.Parameters.AddWithValue("@Sueldo", decimal.Parse(txtSueldo.Text));
            cmd.Parameters.AddWithValue("@EsMoroso", chkMoroso.Checked);

            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Cliente registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar campos
            txtNombrecompleto.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtGarantia.Clear();
            txtSueldo.Clear();
            chkMoroso.Checked = false;

            // Recargar tabla
            CargarClientes();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}