using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechDebtGame.Model
{
    public class Iteration
    {
        public int Number { get; set; }
        public IterationCard Card { get; set; }
        public int TotalCapacity { get; set; }
        public int TechDebtImpactOnCapacity { get; set; }
        public int AvailableCapacity { get; set; }
        public int FeaturePointsComplete { get; set; }
    }
}
