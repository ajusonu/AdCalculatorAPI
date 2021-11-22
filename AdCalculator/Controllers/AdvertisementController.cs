using AdvertisementLibrary.Model;
using AdvertisementLibrary.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AdCalculator.Controllers
{
    /// <summary>
    /// Advertisement Calculator
    /// </summary>
    public class AdvertisementController : ApiController
    {
        /// <summary>
        /// This is Get Process Advertisement Calculation Result
        /// </summary>
        /// <param name="advertisements"></param>
        [HttpPost]
        [Route("api/Advertisement/Process")]
        public async Task<HttpResponseMessage> ProcessAdvertisementCalculation(List<Advertisement> advertisements)
        {
            HttpResponseMessage response;
            string errorContent = "";
            try
            {
                AdvertisementCalculationResult advertisementCalculationResult = await (new AdCalculatorService()).CalculatePrice(advertisements);
                string errorList = advertisementCalculationResult?.Errors.Count() > 0 ? $"Error List:{Environment.NewLine}{ string.Join(Environment.NewLine, advertisementCalculationResult.Errors)}" : string.Empty;
                string warningList = advertisementCalculationResult?.Warnings.Count() > 0 ? $"Warning List:{Environment.NewLine}{ string.Join(Environment.NewLine, advertisementCalculationResult.Warnings)}" : string.Empty;

                if (!advertisementCalculationResult.IsValid)
                {
                    
                    errorContent = $"Failed to validate Payload {Environment.NewLine}{errorList}{Environment.NewLine}{warningList} ";
                    response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(errorContent)
                    };
                }
                else
                {
                    // prepare Success response
                    response = Request.CreateResponse<AdvertisementCalculationResult>(HttpStatusCode.OK, advertisementCalculationResult);
                }
            }
            catch (Exception ex)
            {
                //log error
                errorContent = $"Payload Error";
                response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(errorContent)
                };
            }

           

            return response;

        }
       
    }
}
