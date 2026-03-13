using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reportes.Entities
{
    public class FondoEntidad
    {
        public int FondoId { get; set; }
        public decimal CapitalDisponible { get; set; }
        public decimal TotalPrestado { get; set; }
        public DateTime UltimaActualizacion { get; set; }
    }
}