using AdvertisementLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementLibrary.Service
{
    /// <summary>
    /// Abstract class 
    /// </summary>
    public abstract class BaseAdCalculatorService 
    {
       
        /// <summary>
        /// Calculate Price based on List of Advertisements
        /// </summary>
        /// <param name="advertisements"></param>
        /// <returns></returns>
        public abstract Task<AdvertisementCalculationResult> PerformCalculation();
        /// <summary>
        /// Validate payload for given criteria
        /// </summary>
        /// <returns></returns>
        protected abstract Task<bool> Validate();


    }
}
