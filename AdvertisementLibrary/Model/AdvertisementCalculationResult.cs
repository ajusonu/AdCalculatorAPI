using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertisementLibrary.Model
{
    public class AdvertisementCalculationResult
    {

        /// <summary>
        /// Constructor to initiate 
        /// </summary>
        /// <param name="advertisements"></param>
        public AdvertisementCalculationResult(List<Advertisement> advertisements)
        {
            Advertisements = advertisements;
            Errors = new List<string>();
            Warnings = new List<string>();

        }
        /// <summary>
        /// Check for Validation Result
        /// </summary>
        public bool IsValid { get; set; }
        /// <summary>
        /// Holds any processing errors
        /// </summary>
        public List<string> Errors { get; set; }
        /// <summary>
        /// Holds any processing Warnings
        /// </summary>
        public List<string> Warnings { get; set; }
        /// <summary>
        /// Total Price of Advertisements
        /// </summary>
        public decimal TotalPrice
        {
            get
            { return TotalRunningCharge + TotalLevyCharge + TotalRadioStationSubcriptionCharge;
            }
        }
        /// <summary>
        /// Total Running Chagrg calculated as total Ad Length
        /// </summary>
        public decimal TotalRunningCharge { get; set; }
        /// <summary>
        /// Total Levy Charge as per Advertisement
        /// </summary>
        public decimal TotalLevyCharge { get; set; }
        /// <summary>
        /// Total of Radio Station One Time Charges
        /// </summary>
        public decimal TotalRadioStationSubcriptionCharge { get; set; }
        /// <summary>
        /// List of Advertisement for Advertisement Calculation based on Advertisements
        /// </summary>
        public List<Advertisement> Advertisements { get; set; }
        

    }
}
