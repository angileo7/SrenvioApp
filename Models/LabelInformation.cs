using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SrenvioApp.Models
{
    /// <summary>
    /// Usado para poblar la información que viene del JSON
    /// </summary>
    public class LabelInformation
    { 
        public float length { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        public float weight { get; set; }
        public string distance_unit { get; set; }
        public string mass_unit { get; set; }
    }
}
