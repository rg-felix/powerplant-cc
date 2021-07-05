using System.Text.Json.Serialization;

namespace PowerPlantCodingChallenge.Dto
{
    public class ProductionPlanRequest
    {
        [JsonPropertyName("load")]
        public double Load { get; set; }

        [JsonPropertyName("fuels")]
        public FuelsDto Fuels { get; set; }

        [JsonPropertyName("powerplants")]
        public PowerPlantDto[] PowerPlants { get; set; }
    }
}
