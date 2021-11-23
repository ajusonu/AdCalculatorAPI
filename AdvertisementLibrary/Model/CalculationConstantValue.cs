using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertisementLibrary.Model
{
    public class CalculationConstantValue
    {
        /// <summary>
        /// Constants
        /// </summary>
        protected const int MAX_Advertisement_LENGTH_IN_SECONDS = 180;
        protected const int MIN_Advertisement_LENGTH_IN_SECONDS = 5;

        protected const decimal VIDEO_LEVY_CHARGE_PER_Advertisement = 2.25m;
        protected const decimal RADIO_LEVY_CHARGE_PER_Advertisement = 1.50m;

        protected const decimal RADIO_STAR_WARS_FM_CHARGE = 2.50m;
        protected const decimal RADIO_PLAINS_MEN_FM_CHARGE = 2.50m;
        protected const decimal RADIO_OTHER_CHARGE = 0m;

    }
}
