using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reportes.Entities
{
    public class TablaAmortizacion
    {
        public int AmortizacionId { get; set; }
        public int PrestamoId { get; set; }
        public int NumeroCuota { get; set; }
        public decimal Cuota { get; set; }
        public decimal InteresDelMes { get; set; }
        public decimal CapitalAmortizado { get; set; }
        public decimal SaldoRestante { get; set; }
        public DateTime? FechaPago { get; set; }  // NULL = aún no pagado
        public bool Pagado { get; set; }
    }
}