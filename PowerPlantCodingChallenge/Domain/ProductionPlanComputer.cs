using System.Collections.Generic;
using System.Linq;

namespace PowerPlantCodingChallenge.Domain
{
    public class ProductionPlanComputer : IProductionPlanComputer
    {
        public Dictionary<string, double> ComputeLoad(double load, IEnumerable<PowerPlant> plants)
        {
            var availablePlants = plants
                .OrderBy(o => o.PricePerEffectiveMw)
                .ThenByDescending(o => o.PowerMax)
                .ToArray();

            var openPlants = new HashSet<PowerPlant>();
            var head = new PlantResult();

            var cheapest = (PlantResult)null;

            // main idea is to take plants from the cheapest until we match or exceed the target load or the price of the cheapest
            // then if it is possible to reach the load by putting less power in one or multiple plants, we do so starting with the most expensive
            // then if that is cheaper than the cheapest we found, we save it as the new cheapest
            // the problem I'm trying to avoid is when it is cheaper to start 2 mid price than 1 low price and 1 high price
            // low: 4-4, 1.9$
            // mid: 5-5, 2.0$
            // mid: 5-5, 2.0$
            // high: 6-6, 2.1$
            // low + high = 4 * 1.9 + 6 * 2.1 = 20.2
            // mid + mid = 10 * 2.0 = 20
            var searching = true;
            while (searching)
            {
                // find cheapest available plant not yet visited
                // the check on the price is to avoid permutations of the same solution
                var plant = availablePlants.FirstOrDefault(o => !openPlants.Contains(o) && (head == null || (!head.Visited.Contains(o) && (head.Plant?.PricePerEffectiveMw ?? 0) <= o.PricePerEffectiveMw)));
                if (plant == null)
                {
                    // nothing left to explore
                    if (head.Previous == null)
                        break;

                    // completed this branch, go back to parent
                    openPlants.Remove(head.Plant);
                    head = head.Previous;
                    continue;
                }

                openPlants.Add(plant);
                head.Visited.Add(plant);

                head = new PlantResult(head, plant);

                // avoid exploring if the minimum price for the current branch is higher than the cheapest match
                if ((cheapest?.TotalPrice ?? double.MaxValue) < head.CostMin)
                {
                    openPlants.Remove(head.Plant);
                    head = head.Previous;

                    // we can also stop exporing the parent node because all following plants are more expensive than the one we just visited
                    if (openPlants.Count > 0)
                    {
                        openPlants.Remove(head.Plant);
                        head = head.Previous;
                    }

                    continue;
                }

                // overshot, end the search in that branch
                if (load <= head.PowerMax)
                {
                    // we can reach the target load
                    if (head.PowerMin <= load)
                    {
                        var clone = head.Clone();

                        clone.ComputePower(load);
                        clone.ComputePrice();

                        if (clone.TotalPrice < (cheapest?.TotalPrice ?? double.MaxValue))
                            cheapest = clone;
                    }

                    // pop
                    openPlants.Remove(plant);
                    head = head.Previous;
                }
            }

            var result = availablePlants.ToDictionary(o => o.Name, o => 0.0);
            foreach (var plant in cheapest.Unwrap().ToArray())
                result[plant.Plant.Name] = plant.Power;


            return result;
        }
    }
}
