using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reportes._3.Entity
{
    internal class FondoEntidad
    {
        public int FondoId { get; set; }
        public decimal CapitalDisponible { get; set; }
        public decimal TotalPrestado { get; set; }
        public DateTime UltimaActualizacion { get; set; }
    }
}