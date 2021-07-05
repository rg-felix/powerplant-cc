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

        public static PowerPlant[] CreatePlants(ProductionPlanRequest request)
        {
            return request.PowerPlants.Select(o => CreatePlant(request, o)).ToArray();
        }
        public static PowerPlant CreatePlant(ProductionPlanRequest request, PowerPlantDto plant)
        {
            switch (plant.Type)
            {
                case PlantType.GasFired:
                    return new PowerPlant(plant.Name, request.Fuels.Gas, plant.Efficiency, plant.PMin, plant.PMax);

                case PlantType.TurboJet:
                    return new PowerPlant(plant.Name, request.Fuels.Kerosine, plant.Efficiency, plant.PMin, plant.PMax);

                case PlantType.WindTurbine:
                    return new PowerPlant(plant.Name, 0, plant.Efficiency, plant.PMin * request.Fuels.Wind / 100, plant.PMax * request.Fuels.Wind / 100);

                default:
                    throw new ArgumentException($"Plant type {plant.Type} is not supported.");
            }
        }
    }
}
