using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Reportes._2.Database
{
    internal class Conexion
    {
        private string cadena = "Server=DESKTOP-VE5IE91\\SQLEXPRESS01;Database=Reportes;Integrated Security=True;TrustServerCertificate=True;";

        public SqlConnection ObtenerConexion()
        {
            SqlConnection conn = new SqlConnection(cadena);
            conn.Open();
            return conn;
        }
    }
}