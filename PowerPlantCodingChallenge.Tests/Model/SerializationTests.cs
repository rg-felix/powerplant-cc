using PowerPlantCodingChallenge.Dto;
using PowerPlantCodingChallenge.Tests.Helpers;
using Xunit;

namespace PowerPlantCodingChallenge.Tests
{
    public class SerializationTests
    {
        [Fact]
        public void Deserialize_Payload_Success()
        {
            // arrange & act
            var deserialized = Helper.FromJsonFile("Models/payload0.json");

            // assert

            // load
            Assert.Equal(480, deserialized.Load);

            // fuels
            Assert.NotNull(deserialized.Fuels);
            Assert.Equal(13.4, deserialized.Fuels.Gas);
            Assert.Equal(50.8, deserialized.Fuels.Kerosine);
            Assert.Equal(20, deserialized.Fuels.Co2);
            Assert.Equal(60, deserialized.Fuels.Wind);

            // plants
            Assert.Equal(3, deserialized.PowerPlants.Length);

            // gas
            var plant = deserialized.PowerPlants[0];
            Assert.Equal(0.1, plant.Efficiency);
            Assert.Equal("gasfired1", plant.Name);
            Assert.Equal(200, plant.PMax);
            Assert.Equal(100, plant.PMin);
            Assert.Equal(PlantType.GasFired, plant.Type);

            // turbojet
            plant = deserialized.PowerPlants[1];
            Assert.Equal(0.2, plant.Efficiency);
            Assert.Equal("turbojet1", plant.Name);
            Assert.Equal(400, plant.PMax);
            Assert.Equal(300, plant.PMin);
            Assert.Equal(PlantType.TurboJet, plant.Type);

            // wind
            plant = deserialized.PowerPlants[2];
            Assert.Equal(0.3, plant.Efficiency);
            Assert.Equal("windturbine1", plant.Name);
            Assert.Equal(600, plant.PMax);
            Assert.Equal(500, plant.PMin);
            Assert.Equal(PlantType.WindTurbine, plant.Type);
        }
    }
}
