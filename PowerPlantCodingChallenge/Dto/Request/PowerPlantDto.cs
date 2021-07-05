using System.Text.Json.Serialization;

namespace PowerPlantCodingChallenge.Dto
{
    public class PowerPlantDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public PlantType Type { get; set; }

        [JsonPropertyName("efficiency")]
        public double Efficiency { get; set; }

        [JsonPropertyName("pmin")]
        public int PMin { get; set; }

        [JsonPropertyName("pmax")]
        public int PMax { get; set; }
    }
}
