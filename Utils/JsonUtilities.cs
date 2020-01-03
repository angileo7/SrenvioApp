using Newtonsoft.Json;
using SrenvioApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SrenvioApp.Utils
{
    public  class JsonUtilities
    {
        public static List<Shipment> readJsonFile(string pathToJsonFile)
        {
            List<Shipment> listItems;
            using (StreamReader r = new StreamReader(pathToJsonFile))
            {
                listItems = new List<Shipment>();
                string json = r.ReadToEnd();
                listItems = JsonConvert.DeserializeObject<List<Shipment>>(json);
            }
            return listItems;
        }
    }
}
