using Reportes._4.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void tsmCliente_Click(object sender, EventArgs e)
        {

        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            Clientes frm = new Clientes();
            frm.Show();
        }

        private void btnPrestamos_Click(object sender, EventArgs e)
        {
            Prestamos frm = new Prestamos();
            frm.Show();
        }

        private void btnPagos_Click(object sender, EventArgs e)
        {
            Pagos frm = new Pagos();
            frm.Show();
        }

        private void btnConsultas_Click(object sender, EventArgs e)
        {
            Consultas frm = new Consultas();
            frm.Show();
        }

        private void btnMoras_Click(object sender, EventArgs e)
        {
            Moras frm = new Moras();
            frm.Show();
        }
    }
}