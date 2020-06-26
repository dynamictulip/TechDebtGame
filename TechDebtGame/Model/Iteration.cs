﻿namespace TechDebtGame.Model
{
    public class Iteration
    {
        public int Number { get; set; }
        public TechDebtCard Card { get; set; }
        public int TotalCapacity { get; set; }
        public int TechDebtImpactOnCapacity { get; set; }
        public int AvailableCapacity { get; set; }
        public int FeaturePointsComplete { get; set; }
        public static Iteration Empty => new Iteration {Card = new TechDebtCard()};
    }
}