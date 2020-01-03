using System;
using System.Collections.Generic;
using System.Text;

namespace UtilitiesSrenvio
{
    public class WeightUtilitties
    {
        public static int calculateSobrePeso(int ticketWeight, string realWeight)
        {
            int sobrePeso = 0;
            float diferencia = 0;
            float realW;
            float.TryParse(realWeight, out realW);
            diferencia = realW - (float)ticketWeight;
            if (diferencia > 0)
                sobrePeso = (int)Math.Ceiling(diferencia);
            return sobrePeso;
        }

        /// <summary>
        /// Método para calcular el peso total
        /// </summary>
        /// <param name="volumetricWeight"></param>
        /// <param name="weight"></param>
        /// <returns>regresa un entero redondeabo hacia arriba</returns>
        public static int calculateTotalWeight(float volumetricWeight, float weight)
        {
            var mayorNumber = volumetricWeight > weight ? volumetricWeight : weight;
            return (int)Math.Ceiling(mayorNumber);
        }
        /// <summary>
        /// Método para calcular el peso volumétrico
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static float calculateVolumetricWeight(float width, float height, float length)
        {
            return (width * height * length) / 5000;
        }
    }
}
