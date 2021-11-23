using AdvertisementLibrary.Common;
using AdvertisementLibrary.Constants;
using AdvertisementLibrary.Enums;
using AdvertisementLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementLibrary.Service
{
    /// <summary>
    /// Advertisement Calculator Service
    /// </summary>
    public class AdCalculatorService : IBaseAdCalculatorService, IValidor
    {
        private List<Advertisement> _Advertisements;
        private AdvertisementCalculationResult _AdvertisementCalculationResult;

        /// <summary>
        /// Calculate Price for given payload
        /// </summary>
        /// <param name="advertisements"></param>
        /// <returns></returns>
        public async Task<AdvertisementCalculationResult> CalculatePrice(List<Advertisement> advertisements)
        {
            Initialize(advertisements);
            if (await Validate())
            {
                await PerformCalculation();
            }
            return await Task.FromResult(_AdvertisementCalculationResult);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="advertisements"></param>
        private void Initialize(List<Advertisement> advertisements)
        {
            _AdvertisementCalculationResult = new AdvertisementCalculationResult(advertisements);
            _Advertisements = advertisements;
        }

        /// <summary>
        /// Perform Calculatin based on the Business logic
        /// </summary>
        private async Task<bool> PerformCalculation()
        {
            try
            {
                await CalculateAdvertisementRunningCharge();
                await CalculateTotalLevyCharge();
                await GetTotalRadioStationOneOffCharges();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed {ex.Message} in  inside {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType}");
                _AdvertisementCalculationResult.Errors.Add($"Advertisement calculation failed");
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        /// <summary>
        /// Calculate Total Levy Charge per Advertisement
        /// </summary>
        /// <param name="advertisementCost"></param>
        /// <returns></returns>
        private async Task CalculateTotalLevyCharge()
        {
            decimal totalLevyCharge = 0;
            foreach (Advertisement advertisement in _Advertisements)
            {
                totalLevyCharge += await GetLevyCharge(advertisement.Type);
            }
            _AdvertisementCalculationResult.TotalLevyCharge = totalLevyCharge;
            return;
        }
        /// <summary>
        /// Calculate Total Advertisement Running Charge based on Advertisements Price and Length
        /// </summary>
        /// <param name="advertisementCost"></param>
        /// <returns></returns>
        private async Task CalculateAdvertisementRunningCharge()
        {
            decimal totalAdvertisementRunningCharge = 0;
            foreach (Advertisement advertisement in _Advertisements)
            {
                totalAdvertisementRunningCharge += await new AdvertisementLengthRuleDictionary().GetChargeRate(advertisement.Length)* advertisement.Length;
            }
            _AdvertisementCalculationResult.TotalRunningCharge = totalAdvertisementRunningCharge;
            return;
        }

        /// <summary>
        /// Validate payload for given criteria
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Validate()
        {
            bool isValid = true;
            try
            {
                foreach (Advertisement advertisement in _Advertisements)
                {
                    if (!advertisement.Length.IsInRange(AdConstant.MIN_LENGTH_IN_SECONDS, AdConstant.MAX_LENGTH_IN_SECONDS))
                    {
                        _AdvertisementCalculationResult.Errors.Add($"Advertisement Id {advertisement.Id} Length {advertisement.Length} is invalid. Range is {AdConstant.MIN_LENGTH_IN_SECONDS} to {AdConstant.MAX_LENGTH_IN_SECONDS} ");
                        isValid = false;
                    }
                    if (advertisement.Type == AdvertisementType.Video && advertisement.Station != null)
                    {
                        _AdvertisementCalculationResult.Warnings.Add($"Advertisement Id {advertisement.Id} Radio Station {advertisement.Station.ToString()} is invalid for {AdvertisementType.Video.ToString()}");
                    }
                    if (!Enum.GetValues(typeof(AdvertisementType)).Cast<AdvertisementType>().ToList().Contains(advertisement.Type))
                    {
                        _AdvertisementCalculationResult.Errors.Add($"Advertisement Id {advertisement.Id} Invalid Type {advertisement.Type} ");
                        isValid = false;
                    }
                    if (advertisement.Type == AdvertisementType.Radio && !Enum.GetValues(typeof(RadioStation)).Cast<RadioStation?>().ToList().Contains(advertisement.Station))
                    {
                        _AdvertisementCalculationResult.Errors.Add($"Advertisement Id {advertisement.Id} Invalid Station {advertisement.Station} for Advertisement Type {AdvertisementType.Radio.ToString()}");
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed {ex.Message} in  inside {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType}");
                _AdvertisementCalculationResult.Errors.Add($"Advertisement calculation failed");
                return await Task.FromResult(false);
            }
            _AdvertisementCalculationResult.IsValid = isValid;
            return await Task.FromResult(isValid);
        }
        /// <summary>
        /// Get Levy Charge
        /// </summary>
        /// <param name="advertisement"></param>
        /// <returns></returns>
        private async Task<decimal> GetLevyCharge(AdvertisementType advertisementType)
        {
            decimal levyChargePerAdvertisement;
            switch (advertisementType)
            {
                case AdvertisementType.Video:
                    levyChargePerAdvertisement = AdConstant.VIDEO_LEVY_CHARGE_PER_AD;
                    break;
                case AdvertisementType.Radio:
                    levyChargePerAdvertisement = AdConstant.RADIO_LEVY_CHARGE_PER_AD;
                    break;
                default:
                    levyChargePerAdvertisement = 0;
                    break;
            }

            return await Task.FromResult(levyChargePerAdvertisement);
        }
      
        /// <summary>
        /// Based on Distinct Radio Stations in Adversitement List Get Total One Type Cost
        /// </summary>
        /// <param name="radioStations"></param>
        /// <returns></returns>
        private async Task<bool> GetTotalRadioStationOneOffCharges()
        {

            decimal totalRadioStationSubcriptionCharge = 0;
            foreach (RadioStation radioStation in await Task.FromResult(_Advertisements.FindAll(a => a.Station != null).GroupBy(r => r.Station).Select(g => g.Key).ToList()))
            {
                switch (radioStation)
                {
                    case RadioStation.STAR_WARS_FM:
                        totalRadioStationSubcriptionCharge += AdConstant.RADIO_STAR_WARS_FM_CHARGE;
                        break;
                    case RadioStation.PLAINS_MEN_FM:
                        totalRadioStationSubcriptionCharge += AdConstant.RADIO_PLAINS_MEN_FM_CHARGE;
                        break;
                    default:
                        totalRadioStationSubcriptionCharge += AdConstant.RADIO_OTHER_CHARGE;
                        break;
                }
            }
            _AdvertisementCalculationResult.TotalRadioStationSubcriptionCharge = totalRadioStationSubcriptionCharge;
            return await Task.FromResult(true);
        }
    }
}
