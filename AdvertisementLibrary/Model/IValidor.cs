using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementLibrary.Service
{
    interface IValidor
    {
        /// <summary>
        /// Validate payload for given criteria
        /// </summary>
        /// <returns></returns>
        Task<bool> Validate();
    }
}
