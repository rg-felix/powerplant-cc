using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerPlantCodingChallenge.Domain
{
    public class PlantResult
    {
        public PlantResult()
        {
            PowerMax = 0;
            PowerMin = 0;
        }
        public PlantResult(PlantResult previous, PowerPlant plant)
        {
            Previous = previous;
            Plant = plant;

            PowerMin = previous.PowerMin + plant.PowerMin;
            PowerMax = previous.PowerMax + plant.PowerMax;

            CostMin = previous.CostMin + plant.CostMin;
            CostMax = previous.CostMax + plant.CostMax;
        }

        public PlantResult Previous { get; set; }

        public PowerPlant Plant { get; set; }
        public HashSet<PowerPlant> Visited { get; }
            = new HashSet<PowerPlant>();

        public double PowerMin { get; }
        public double PowerMax { get; }

        public double CostMin { get; }
        public double CostMax { get; }

        public double Power { get; set; }
        public double Price { get; set; }
        public double TotalPrice
            => Price + Previous?.TotalPrice ?? 0;

        public void ComputePower(double load)
        {
            if (load < PowerMax)
            {
                Power = Math.Max(Plant.PowerMin, load - Previous?.PowerMax ?? 0);
            }
            else
            {
                Power = Plant?.PowerMax ?? 0;
            }

            Previous?.ComputePower(load - Power);
        }
        public void ComputePrice()
        {
            Price = Power * Plant?.PricePerEffectiveMw ?? 0;
            Previous?.ComputePrice();
        }

        public PlantResult Clone()
        {
            if (Previous == null)
                return new PlantResult();

            else
                return new PlantResult(Previous.Clone(), Plant);
        }

        public IEnumerable<PlantResult> Unwrap()
        {
            if (Previous == null)
                return Enumerable.Empty<PlantResult>();

            else
                return new[] { this }.Union(Previous.Unwrap());
        }
    }
}
