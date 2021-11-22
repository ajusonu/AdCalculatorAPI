using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertisementLibrary.Common
{
    public static class ExtensionMethod
    {
        /// <summary>
        /// Integer Extension method to check range
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        public static bool IsInRange(this int value, int minimum, int maximum)
        {
            return value >= minimum && value <= maximum;
        }

    }
}
