namespace PowerPlantCodingChallenge.Domain
{
    public class PowerPlant
    {
        public PowerPlant(string name, double fuelPrice, double efficiency, double pMin, double pMax, double co2PricePerMw = 0)
        {
            Name = name;
            Efficiency = efficiency;
            PowerMin = pMin;
            PowerMax = pMax;

            PricePerEffectiveMw = fuelPrice / Efficiency + co2PricePerMw;
            CostMin = PowerMin * PricePerEffectiveMw;
            CostMax = PowerMax * PricePerEffectiveMw;
        }

        public string Name { get; set; }
        public double Efficiency { get; set; }
        public double PowerMin { get; set; }
        public double PowerMax { get; set; }

        public double PricePerEffectiveMw { get; }

        public double CostMin { get; }
        public double CostMax { get; }
    }
}
