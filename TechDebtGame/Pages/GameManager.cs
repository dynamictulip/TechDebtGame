using System.Collections.Generic;
using System.Linq;
using TechDebtGame.Model;

namespace TechDebtGame.Pages
{
    public class GameManager
    {
        private const int TeamTotalCapacity = 60;

        public readonly OutstandingFeaturesCardList OutstandingFeaturesCardList = new OutstandingFeaturesCardList();
        public readonly OutstandingTechDebtCardList OutstandingTechDebtCardList = new OutstandingTechDebtCardList();
        public readonly ProposedForIterationCardList ProposedForIterationCardList = new ProposedForIterationCardList();
        private readonly IterationCardDeck _iterationCardDeck = new IterationCardDeck();

        public GameManager()
        {
            StartNewIteration();
        }

        public List<IterationModel> Iterations { get; } = new List<IterationModel>();
        public IterationModel CurrentIteration => Iterations.Last();
        public IterationModel LastIteration => Iterations.Count > 1 ? Iterations[^2] : IterationModel.Empty;

        public void StartNewIteration()
        {
            ProposedForIterationCardList.Clear();

            if (Iterations.Any())
                OutstandingTechDebtCardList.Add(CurrentIteration.GameCardModel);

            var currentTechDebt = OutstandingTechDebtCardList.TechDebtImpact;

            Iterations.Add(new IterationModel
            {
                Number = Iterations.Count + 1,
                GameCardModel = _iterationCardDeck.NextScenario,
                TotalCapacity = TeamTotalCapacity,
                TechDebtImpactOnCapacity = currentTechDebt,
                AvailableCapacity = TeamTotalCapacity + currentTechDebt
            });
        }

        public void UpdateCurrentIteration()
        {
            CurrentIteration.AvailableCapacity =
                CurrentIteration.TechDebtImpactOnCapacity
                + CurrentIteration.TotalCapacity
                - ProposedForIterationCardList.Cost;

            CurrentIteration.FeaturePointsComplete = ProposedForIterationCardList.FeaturePoints;
        }


        public bool MoveCard(IGameCardModel cardModel, GameCardListModel listToMoveTo)
        {
            if (!listToMoveTo.WillAccept(cardModel))
                return false;

            ProposedForIterationCardList.Remove(cardModel);
            OutstandingFeaturesCardList.Remove(cardModel);
            OutstandingTechDebtCardList.Remove(cardModel);

            listToMoveTo.Add(cardModel);

            UpdateCurrentIteration();

            return true;
        }
    }
}