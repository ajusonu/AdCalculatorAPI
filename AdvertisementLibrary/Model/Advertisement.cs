using AdvertisementLibrary.Common;
using AdvertisementLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementLibrary.Model
{
    /// <summary>
    /// Advertisement
    /// </summary>
    public class Advertisement 
    {
        public int Id { get; set; }
        /// <summary>
        /// Advertisement Length in Secs
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// AdvertisementType
        /// </summary>
        public AdvertisementType Type { get; set; }
        /// <summary>
        /// Radio Stations
        /// </summary>
        public RadioStation? Station { get; set; }
        
    }
}
