using PowerPlantCodingChallenge.Domain;
using PowerPlantCodingChallenge.Dto;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PowerPlantCodingChallenge.Tests.Helpers
{
    public static class Helper
    {
        public static ProductionPlanRequest FromJsonFile(string path)
        {
            if (!path.StartsWith("Payloads"))
                path = Path.Join("Payloads", path);

            var serializationOptions = new JsonSerializerOptions();
            serializationOptions.Converters.Add(new JsonStringEnumConverter());

            return JsonSerializer.Deserialize<ProductionPlanRequest>(File.ReadAllText(path), serializationOptions);
        }
    }
}
