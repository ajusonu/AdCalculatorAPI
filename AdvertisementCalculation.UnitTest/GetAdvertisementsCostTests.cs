using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisementLibrary.Enums;
using AdvertisementLibrary.Model;
using AdvertisementLibrary.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.AdvertisementCalculation
{
    /// <summary>
    /// Test class to exercise the advertisement calculation.  
    /// </summary>
    [TestClass]
    public class GetAdvertisementsCostTests
    {
        /// <summary>
        /// Test method
        /// </summary>
        [TestMethod]
        public async Task TestMethod_PerformCalculation_InValid()
        {
            //arrange
            List<Advertisement> advertisements = new List<Advertisement>()
            {

            };

            advertisements.Add(new Advertisement() { Id = 1, Length = 10, Station = RadioStation.PLAINS_MEN_FM, Type = AdvertisementType.Radio });
            advertisements.Add(new Advertisement() { Id = 2, Length = 100, Type = AdvertisementType.Video });
            advertisements.Add(new Advertisement() { Id = 3, Length = 1, Type = AdvertisementType.Video });
            advertisements.Add(new Advertisement() { Id = 3, Length = 1, Type = AdvertisementType.Video, Station = RadioStation.OTHER });

            AdCalculatorService adCalculatorService = new AdCalculatorService(advertisements);

            //action
            AdvertisementCalculationResult result = await adCalculatorService.PerformCalculation();

            //Assert
            Assert.AreEqual(result.TotalPrice, 0m);

        }
        /// <summary>
        /// Valid test with one Video and one Radio
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestMethod_PerformCalculation_Valid()
        {
            //arrange
            List<Advertisement> advertisements = new List<Advertisement>()
            {

            };

            advertisements.Add(new Advertisement() { Id = 1, Length = 10, Station = RadioStation.STAR_WARS_FM, Type = AdvertisementType.Radio });
            advertisements.Add(new Advertisement() { Id = 2, Length = 100, Type = AdvertisementType.Video });
            
            AdCalculatorService adCalculatorService = new AdCalculatorService(advertisements);

            //action
            AdvertisementCalculationResult result = await adCalculatorService.PerformCalculation();

            //Assert
            Assert.AreEqual(result.TotalPrice, 113.75m);

        }
        /// <summary>
        /// Valid test with two Video 1 with Radio Station to test warning 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestMethod_PerformCalculation_Valid_2Video_1InvalidVideo()
        {
            //arrange
            List<Advertisement> advertisements = new List<Advertisement>()
            {

            };

            advertisements.Add(new Advertisement() { Id = 1, Length = 10, Station = RadioStation.STAR_WARS_FM, Type = AdvertisementType.Video });
            advertisements.Add(new Advertisement() { Id = 2, Length = 100, Type = AdvertisementType.Video });

            AdCalculatorService adCalculatorService = new AdCalculatorService(advertisements);

            //action
            AdvertisementCalculationResult result = await adCalculatorService.PerformCalculation();

            //Assert
            Assert.AreEqual(result.TotalPrice, 112m);
            Assert.IsTrue(result.Warnings.Count > 0);

        }
    }
}
