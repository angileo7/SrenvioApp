using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SrenvioApp.Models;
using SrenvioApp.Utils;
using UtilitiesSrenvio;

namespace SrenvioApp.Controllers
{
    public class HomeController : Controller
    {
        private List<Reporte> listing;
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            listing = new List<Reporte>();
            _configuration = configuration;
        }

        public async Task<JsonResult> IndexAsync()
        {
            Reporte reportItem;
            List <Reporte>  listaReport = new List<Reporte>();
            //Recuperamos información de guías desde un archivo en formato JSON
            var itemsInJsonFile = JsonUtilities.readJsonFile("Data\\labels.json");
            //Cálculo de peso volumétrico de cáda item que se encuentra en el Json, este cálculo se guarda un la propiedad del objeto llamada volumetricWeight
            foreach (var shipment in itemsInJsonFile)
            {
                shipment.volumetricWeight =WeightUtilitties.calculateVolumetricWeight(shipment.parcel.width, shipment.parcel.height, shipment.parcel.length);
                //Decidimos el peso total del item y lo metemos en el reporte
                reportItem = new Reporte { ticketWeight = WeightUtilitties.calculateTotalWeight(shipment.volumetricWeight, shipment.parcel.weight) };
                //Llamada al servicio asyncrona
                Task<string> soapRequest = CreateSoapEnvelopeAsync(
                _configuration.GetValue<string>("Key"),
                _configuration.GetValue<string>("Password"),
                _configuration.GetValue<string>("AccountNumber"),
                _configuration.GetValue<string>("MeterNumber"),
                shipment.tracking_number
                );
                //Recuperamos informacion del peso real del servicio
                reportItem.realWeight = await soapRequest;
                //Calculamos si existe el sobre peso
                reportItem.sobrePeso = WeightUtilitties.calculateSobrePeso(reportItem.ticketWeight, reportItem.realWeight);
                if (reportItem.sobrePeso > 0)
                    reportItem.hasSobrePeso = true;
                listaReport.Add(reportItem);
            }
            
            return Json(new { listaReport });
        }

        /// <summary>
        /// Método para crear el xml del tracking, basadi en Example 41: Track Request (Track By Number)
        /// </summary>
        /// <param name="key">key credential</param>
        /// <param name="password">password credential</param>
        /// <param name="accountNumber">account number credential</param>
        /// <param name="meterNumber">meter number credential</param>
        /// <param name="trackingNumber">tracking number credential</param>
        /// <returns></returns>
        public async Task<string> CreateSoapEnvelopeAsync(string key, string password, string accountNumber, string meterNumber, string trackingNumber)
        {
            string content = "";
            string soapString = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
          <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:v16=""http://fedex.com/ws/track/v16""
            <soapenv:Header/>      
            <soapenv:Body>
                <v16:TrackRequest>
                    <v16:WebAuthenticationDetail>
                        <v16:ParentCredential>
                            <v16:Key>""{0}""</v16:Key>
                            <v16:Password>""{1}""</v16:Password>
                        </v16:ParentCredential>
                        <v16:UserCredential>
                            <v16:Key>""{0}""</v16:Key>
                            <v16:Password>""{1}""</v16:Password>
                        </v16:UserCredential>
                    </v16:WebAuthenticationDetail>
                    <v16:ClientDetail>
                        <v16:AccountNumber>""{3}""</v16:AccountNumber>
                        <v16:MeterNumber>""{4}""</v16:MeterNumber>
                    </v16:ClientDetail>
                    <v16:TransactionDetail> 
                        <v16:CustomerTransactionId>Track By Number_v16</v16:CustomerTransactionId> 
                        <v16:Localization> 
                             <v16:LanguageCode>EN</v16:LanguageCode>
                             <v16:LocaleCode>US</v16:LocaleCode>
                        </v16:Localization> 
                    </v16:TransactionDetail>
                    <v16:Version>      
                        <v16:ServiceId>trck</v16:ServiceId>    
                        <v16:Major>16</v16:Major>            
                        <v16:Intermediate>0</v16:Intermediate>       
                        <v16:Minor>0</v16:Minor>         
                    </v16:Version> 
                    <v16:SelectionDetails>  
                        <v16:CarrierCode>FDXE</v16:CarrierCode>  
                        <v16:PackageIdentifier>               
                            <v16:Type>TRACKING_NUMBER_OR_DOORTAG</v16:Type>    
                            <v16:Value>""{4}""</v16:Value>       
                        </v16:PackageIdentifier>                    
                        <v16:ShipmentAccountNumber/>             
                        <v16:SecureSpodAccount/>       
                            <v16:Destination>          
                                <v16:GeographicCoordinates>rates evertitque aequora</v16:GeographicCoordinates>       
                            </v16:Destination>       
                   </v16:SelectionDetails>    
                </v16:TrackRequest>   
            </soapenv:Body>
          </soapenv:Envelope>", key, password, accountNumber, meterNumber, trackingNumber);
            try
            {
                HttpResponseMessage response = await PostXmlRequest("https://wsbeta.fedex.com/web-services/track", soapString);
                content = await response.Content.ReadAsStringAsync();
            }
            catch(Exception ex)
            {
                content = ex.InnerException.Message;
            }

            return content;
        }

        public static async Task<HttpResponseMessage> PostXmlRequest(string baseUrl, string xmlString)
        {
            using (var httpClient = new HttpClient())
            {
                var httpContent = new StringContent(xmlString, Encoding.UTF8, "text/xml");

                return await httpClient.PostAsync(baseUrl, httpContent);
            }
        }
    }
}
