using System.Collections.Generic;
using System.Linq;
using TechDebtGame.Model;

namespace TechDebtGame.Pages
{
    internal class IterationManager
    {
        private const int TeamTotalCapacity = 60;
        private readonly CardManager _cardManager;

        public IterationManager(CardManager cardManager)
        {
            _cardManager = cardManager;
            Iterations = new List<Iteration>();
            StartNewIteration();
        }

        public List<Iteration> Iterations { get; private set; }
        public Iteration CurrentIteration => Iterations.Last();
        public Iteration LastIteration => Iterations.Count > 1 ? Iterations[^2] : Iteration.Empty;

        public void StartNewIteration()
        {
            var currentTechDebt = -35;
            var availableCapacity = TeamTotalCapacity - currentTechDebt;
            var randomScenario = _cardManager.IterationCards.Pop();

            Iterations.Add(new Iteration
            {
                Number = Iterations.Count + 1,
                GameCard = randomScenario,
                TotalCapacity = TeamTotalCapacity,
                TechDebtImpactOnCapacity = currentTechDebt,
                AvailableCapacity = availableCapacity
            });
        }
    }
}