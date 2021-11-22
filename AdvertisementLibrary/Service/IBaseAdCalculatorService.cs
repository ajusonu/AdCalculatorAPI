using AdvertisementLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementLibrary.Service
{
    public interface IBaseAdCalculatorService
    {
        /// <summary>
        /// Calculate Price based on List of Advertisements
        /// </summary>
        /// <param name="advertisements"></param>
        /// <returns></returns>
        Task<AdvertisementCalculationResult> CalculatePrice(List<Advertisement> advertisements);

      
    }
}
