using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reportes.Entities
{
    public class Prestamo
    {
        public int PrestamoId { get; set; }
        public int ClienteId { get; set; }
        public decimal Monto { get; set; }
        public int PlazoMeses { get; set; }
        public decimal TasaInteres { get; set; }
        public decimal Interes { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal CuotaMensual { get; set; }
        public int CantidadMoras { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Estado { get; set; } // Activo / Pagado / Moroso
    }
}