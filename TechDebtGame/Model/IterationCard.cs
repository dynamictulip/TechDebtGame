using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechDebtGame.Model
{
    public class IterationCard
    {
        public string Scenario { get; set; }

        public TechDebtCard TechDebtCard { get; set; }
    }
}
