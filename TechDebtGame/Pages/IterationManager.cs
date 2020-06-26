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
            Iterations = new List<IterationModel>();
            StartNewIteration();
        }

        public List<IterationModel> Iterations { get; private set; }
        public IterationModel CurrentIteration => Iterations.Last();
        public IterationModel LastIteration => Iterations.Count > 1 ? Iterations[^2] : IterationModel.Empty;

        public void StartNewIteration()
        {
            if (Iterations.Any())
                _cardManager.OutstandingTechDebt.Add(CurrentIteration.GameCardModel);

            var currentTechDebt = _cardManager.OutstandingTechDebt.Sum(d => d.Impact);
            var availableCapacity = TeamTotalCapacity + currentTechDebt;
            var randomScenario = _cardManager.IterationCards.Pop();

            Iterations.Add(new IterationModel
            {
                Number = Iterations.Count + 1,
                GameCardModel = randomScenario,
                TotalCapacity = TeamTotalCapacity,
                TechDebtImpactOnCapacity = currentTechDebt,
                AvailableCapacity = availableCapacity
            });
        }
    }
}