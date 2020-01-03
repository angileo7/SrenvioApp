using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SrenvioApp.Models
{
    public class Reporte
    {
        public int ticketWeight { get; set; }
        public string realWeight { get; set; }
        public int sobrePeso { get; set; }
        public bool hasSobrePeso { get; set; }
    }
}
