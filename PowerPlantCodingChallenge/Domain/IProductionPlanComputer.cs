using System.Collections.Generic;

namespace PowerPlantCodingChallenge.Domain
{
    public interface IProductionPlanComputer
    {
        Dictionary<string, double> ComputeLoad(double load, IEnumerable<PowerPlant> plants);
    }
}