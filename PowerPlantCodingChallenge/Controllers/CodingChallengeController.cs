using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PowerPlantCodingChallenge.Domain;
using PowerPlantCodingChallenge.Dto;
using PowerPlantCodingChallenge.Dto.Response;
using System;
using System.Linq;

namespace PowerPlantCodingChallenge.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CodingChallengeController : ControllerBase
    {
        private readonly ILogger<CodingChallengeController> _logger;
        private readonly IProductionPlanComputer productionPlanComputer;

        public CodingChallengeController(
            ILogger<CodingChallengeController> logger,
            IProductionPlanComputer productionPlanComputer)
        {
            _logger = logger;
            this.productionPlanComputer = productionPlanComputer;
        }

        [HttpPost]
        public PowerPlantPowerDto[] ProductionPlan(ProductionPlanRequest request)
        {
            return productionPlanComputer
                .ComputeLoad(request.Load, CreatePlants(request))
                .Select(o => new PowerPlantPowerDto { Name = o.Key, Power = o.Value })
                .ToArray();
        }
        [HttpPost]
        public PowerPlantPowerDto[] ProductionPlanWithCo2(ProductionPlanRequest request)
        {
            return productionPlanComputer
                .ComputeLoad(request.Load, CreatePlantsWithCo2(request))
                .Select(o => new PowerPlantPowerDto { Name = o.Key, Power = o.Value })
                .ToArray();
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
        public static PowerPlant[] CreatePlantsWithCo2(ProductionPlanRequest request)
        {
            return request.PowerPlants.Select(o => CreatePlantWithCo2(request, o)).ToArray();
        }
        public static PowerPlant CreatePlantWithCo2(ProductionPlanRequest request, PowerPlantDto plant)
        {
            switch (plant.Type)
            {
                case PlantType.GasFired:
                    return new PowerPlant(plant.Name, request.Fuels.Gas, plant.Efficiency, plant.PMin, plant.PMax);

                case PlantType.TurboJet:
                    return new PowerPlant(plant.Name, request.Fuels.Kerosine, plant.Efficiency, plant.PMin, plant.PMax, 0.3);

                case PlantType.WindTurbine:
                    return new PowerPlant(plant.Name, 0, plant.Efficiency, plant.PMin * request.Fuels.Wind / 100, plant.PMax * request.Fuels.Wind / 100);

                default:
                    throw new ArgumentException($"Plant type {plant.Type} is not supported.");
            }
        }
    }
}
