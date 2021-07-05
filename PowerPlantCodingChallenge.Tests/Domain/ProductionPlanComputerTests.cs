using PowerPlantCodingChallenge.Domain;
using PowerPlantCodingChallenge.Tests.Helpers;
using System.Collections.Generic;
using Xunit;

namespace PowerPlantCodingChallenge.Tests.Domain
{
    public class ProductionPlanComputerTests
    {
        [Fact]
        public void Payload1_Success()
        {
            // arrange
            var request = Helper.FromJsonFile("payload1.json");
            var plants = Helper.CreatePlants(request);

            var sut = new ProductionPlanComputer();

            // act
            var result = sut.ComputeLoad(request.Load, plants);

            // assert
            var expected = new Dictionary<string, double>
            {
                ["windpark1"] = 90,
                ["windpark2"] = 21.6,
                ["gasfiredbig1"] = 368.4,
                ["gasfiredbig2"] = 0,
                ["gasfiredsomewhatsmaller"] = 0,
                ["tj1"] = 0,
            };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Payload2_Success()
        {
            // arrange
            var request = Helper.FromJsonFile("payload2.json");
            var plants = Helper.CreatePlants(request);

            var sut = new ProductionPlanComputer();

            // act
            var result = sut.ComputeLoad(request.Load, plants);

            // assert
            var expected = new Dictionary<string, double>
            {
                ["windpark1"] = 0,
                ["windpark2"] = 0,
                ["gasfiredbig1"] = 380,
                ["gasfiredbig2"] = 100,
                ["gasfiredsomewhatsmaller"] = 0,
                ["tj1"] = 0,
            };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Payload3_Success()
        {
            // arrange
            var request = Helper.FromJsonFile("payload3.json");
            var plants = Helper.CreatePlants(request);

            var sut = new ProductionPlanComputer();

            // act
            var result = sut.ComputeLoad(request.Load, plants);

            // assert
            var expected = new Dictionary<string, double>
            {
                ["windpark1"] = 90,
                ["windpark2"] = 21.6,
                ["gasfiredbig1"] = 460,
                ["gasfiredbig2"] = 338.4,
                ["gasfiredsomewhatsmaller"] = 0,
                ["tj1"] = 0,
            };
            Assert.Equal(expected, result);
        }

    }
}
