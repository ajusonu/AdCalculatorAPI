using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertisementLibrary.Model
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        /// <summary>
        /// List of Advertisement for Advertisement Calculation based on Advertisements
        /// </summary>
        public List<Advertisement> GetAdvertisements(List<Advertisement> advertisements)
        {
            return advertisements;
        }
    }
}
