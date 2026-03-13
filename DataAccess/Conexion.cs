using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Reportes.DataAccess
{
    public class Conexion
    {
        // Lee la cadena de conexión del App.config
        private static string cadena = ConfigurationManager
            .ConnectionStrings["ReportesDB"].ConnectionString;

        // Retorna una conexión abierta lista para usar
        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }
    }
}