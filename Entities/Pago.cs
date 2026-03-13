using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reportes.Entities
{
    public class Pago
    {
        public int PagoId { get; set; }
        public int PrestamoId { get; set; }
        public int NumeroCuota { get; set; }
        public decimal MontoAnterior { get; set; }
        public decimal InteresPagado { get; set; }
        public decimal NuevoMonto { get; set; }
        public decimal CuotaPagada { get; set; }
        public int MesesRestantes { get; set; }
        public decimal TotalInteresAcum { get; set; }
        public decimal TasaPrestamo { get; set; }
        public bool TuveMora { get; set; }
        public decimal MontoMora { get; set; }
        public DateTime FechaPago { get; set; }
    }
}