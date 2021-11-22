using AdvertisementLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertisementLibrary.AdvertisementService
{
    public interface IAdvertisementService
    {
        List<Advertisement> GetAdvertisements();
    }
}
