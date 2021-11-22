using AdvertisementLibrary.Common;
using AdvertisementLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementLibrary.Model
{
    /// <summary>
    /// This is for all the business logic for Advertisement Calculation
    /// </summary>
    public class AdsCalculator : AdvertisementCalculationResult
    {
        /// <summary>
        /// AdvertisementCalculation
        /// </summary>
        /// <param name="advertisements"></param>
        public AdsCalculator(List<Advertisement> advertisements) : base(advertisements)
        {
            Advertisements = advertisements;
            Errors = new List<string>();
            Warnings = new List<string>();
        }

        /// <summary>
        /// Get Total Advertisements Cost for given list of advertisements 
        /// It will the validation of advertisements and will generate errors or warnings
        /// </summary>
        /// <param name="advertisements"></param>
        /// <returns></returns>
        public async Task<AdvertisementCalculationResult> CalculatePrice()
        {
            try
            {
                if (ValidateAdvertisements())
                {
                    decimal advertisementCost = 0;

                    foreach (Advertisement advertisement in Advertisements)
                    {
                        decimal chargePerSecond = await GetChargePerSecond(advertisement.Length);
                        decimal levyChargePerAdvertisement = GetLevyCharge(advertisement.Type);
                        advertisementCost += levyChargePerAdvertisement + advertisement.Length * chargePerSecond;
                    }
                    advertisementCost += await GetTotalRadioStationOneOffCharges(Advertisements);

                    this.AdvertisementsCost = advertisementCost;
                }
          
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed {ex.Message} in  inside {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType}");
                this.Errors.Add($"Advertisement calculation failed");
                return this;
            }
            return this;
        }
        private bool ValidateAdvertisements()
        {
            bool isValid = true;
            foreach (Advertisement advertisement in Advertisements)
            {
                if(!advertisement.Length.IsInRange(MIN_Advertisement_LENGTH_IN_SECONDS, MAX_Advertisement_LENGTH_IN_SECONDS))
                {
                    this.Errors.Add($"Advertisement Id {advertisement.Id} Length {advertisement.Length} is invalid. Range is {MIN_Advertisement_LENGTH_IN_SECONDS} to {MAX_Advertisement_LENGTH_IN_SECONDS} ");
                    isValid = false;
                }
                if (advertisement.Type == AdvertisementType.Video && advertisement.Station != null)
                {
                    this.Warnings.Add($"Advertisement Id {advertisement.Id} Radio Station {advertisement.Station.ToString()} is invalid for {AdvertisementType.Video.ToString()}");
                }
                if(!Enum.GetValues(typeof(AdvertisementType)).Cast<AdvertisementType>().ToList().Contains(advertisement.Type))
                {
                    this.Warnings.Add($"Advertisement Id {advertisement.Id} Invalid Type {advertisement.Type} ");
                }
                if (advertisement.Type == AdvertisementType.Radio && !Enum.GetValues(typeof(RadioStation)).Cast<RadioStation?>().ToList().Contains(advertisement.Station))
                {
                    this.Warnings.Add($"Advertisement Id {advertisement.Id} Invalid Station {advertisement.Station} for Advertisement Type {AdvertisementType.Radio.ToString()}");
                }
            }
            return isValid;

        }
        /// <summary>
        /// Get Levy Charge
        /// </summary>
        /// <param name="advertisement"></param>
        /// <returns></returns>
        private decimal GetLevyCharge(AdvertisementType advertisementType)
        {
            decimal levyChargePerAdvertisement;
            switch (advertisementType)
            {
                case AdvertisementType.Video:
                    levyChargePerAdvertisement = VIDEO_LEVY_CHARGE_PER_Advertisement;
                    break;
                case AdvertisementType.Radio:
                    levyChargePerAdvertisement = RADIO_LEVY_CHARGE_PER_Advertisement;
                    break;
                default:
                    levyChargePerAdvertisement = 0;
                    break;
            }

            return levyChargePerAdvertisement;
        }
        /// <summary>
        /// Get Charge Per Second
        /// </summary>
        /// <param name="advertisement"></param>
        /// <returns></returns>
        private async Task<decimal> GetChargePerSecond(int advertisementLength)
        {
            decimal chargePerSecond;

            chargePerSecond = (await Task.FromResult(advertisementLength.IsInRange(5, 30))) ? 1.25m : 0;
            chargePerSecond = (await Task.FromResult(advertisementLength.IsInRange(31, 60))) ? 1.15m : chargePerSecond;
            chargePerSecond = (await Task.FromResult(advertisementLength.IsInRange(61, 180))) ? 0.95m : chargePerSecond;

            return chargePerSecond;
        }
        /// <summary>
        /// Based on Distinct Radio Stations in Adversitement List Get Total One Type Cost
        /// </summary>
        /// <param name="radioStations"></param>
        /// <returns></returns>
        private async Task<decimal> GetTotalRadioStationOneOffCharges(List<Advertisement> advertisements)
        {

            decimal radioStationOneOffCharge = 0;
            foreach (RadioStation radioStation in await Task.FromResult(advertisements.FindAll(a => a.Station != null).GroupBy(r => r.Station).Select(g => g.Key).ToList()))
            {
                switch (radioStation)
                {
                    case RadioStation.STAR_WARS_FM:
                        radioStationOneOffCharge += RADIO_STAR_WARS_FM_CHARGE;
                        break;
                    case RadioStation.PLAINS_MEN_FM:
                        radioStationOneOffCharge += RADIO_PLAINS_MEN_FM_CHARGE;
                        break;
                    default:
                        radioStationOneOffCharge += RADIO_OTHER_CHARGE;
                        break;
                }
            }

            return radioStationOneOffCharge;
        }

    }
}
