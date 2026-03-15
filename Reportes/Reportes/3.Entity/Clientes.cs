using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reportes._3.Entity
{
    internal class Clientes
    {
        public int ClienteId { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Garantia { get; set; }
        public decimal Sueldo { get; set; }
        public bool EsMoroso { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}