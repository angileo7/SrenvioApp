using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SrenvioApp.Models
{
    /// <summary>
    /// Usado para poblar la información que viene del JSON
    /// </summary>
    public class Shipment
    {
        public string tracking_number { get; set; }
        public string carrier { get; set; }
        public LabelInformation parcel { get; set; }
        public float volumetricWeight { get; set; }
        public int totalWeight { get; set; }
    }
}
