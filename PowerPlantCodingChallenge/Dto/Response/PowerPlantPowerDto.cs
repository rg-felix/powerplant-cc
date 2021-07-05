using System.Text.Json.Serialization;

namespace PowerPlantCodingChallenge.Dto.Response
{
    public class PowerPlantPowerDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("p")]
        public double Power { get; set; }
    }
}
