using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertisementLibrary.Model
{
    /// <summary>
    /// Building a repository for Advertisement
    /// </summary>
    public interface IAdvertisementRepository
    {
        /// <summary>
        /// Get all the Advertisements
        /// </summary>
        /// <returns></returns>
        List<Advertisement> GetAdvertisements(List<Advertisement> advertisements);
    }
}
