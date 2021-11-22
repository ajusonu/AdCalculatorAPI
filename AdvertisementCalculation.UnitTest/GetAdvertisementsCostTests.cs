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
        /// Pass the data rows to test
        /// </summary>
        [TestMethod]
        //[DataRow(new List<Advertisement>().Add(new Advertisement() { Id = 1, Length = 45, Station = RadioStation.PLAINS_MEN_FM, Type = AdvertisementType.Radio })), ]
        public async Task TestMethod_CalculatePrice()
        {
            //arrange
            List<Advertisement> advertisements = new List<Advertisement>()
            {

            };

            advertisements.Add(new Advertisement() { Id = 1, Length = 10, Station = RadioStation.PLAINS_MEN_FM, Type = AdvertisementType.Radio });
            advertisements.Add(new Advertisement() { Id = 2, Length = 100, Type = AdvertisementType.Video });
            advertisements.Add(new Advertisement() { Id = 3, Length = 1, Type = AdvertisementType.Video });
            advertisements.Add(new Advertisement() { Id = 3, Length = 1, Type = AdvertisementType.Video, Station = RadioStation.OTHER });

            AdCalculatorService adCalculatorService = new AdCalculatorService();

            //action

            AdvertisementCalculationResult result =await adCalculatorService.CalculatePrice(advertisements);

            //assert
            Assert.IsNotNull(result);

        }
    }
}
