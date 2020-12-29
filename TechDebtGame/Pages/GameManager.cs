using System.Collections.Generic;
using System.Linq;
using TechDebtGame.Model;

namespace TechDebtGame.Pages
{
    public class GameManager
    {
        private const int TeamTotalCapacity = 60;
        private readonly IterationCardDeck _iterationCardDeck = new IterationCardDeck();

        public GameManager()
        {
            StartNewIteration();
        }

        public OutstandingFeaturesCardList OutstandingFeaturesCardList { get; } = new OutstandingFeaturesCardList();
        public OutstandingTechDebtCardList OutstandingTechDebtCardList { get; } = new OutstandingTechDebtCardList();
        public ProposedForIterationCardList ProposedForIterationCardList { get; } = new ProposedForIterationCardList();
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

        private void UpdateCurrentIteration()
        {
            CurrentIteration.AvailableCapacity = CalculateAvailableCapacity();
            CurrentIteration.FeaturePointsComplete = ProposedForIterationCardList.FeaturePoints;
        }

        private int CalculateAvailableCapacity()
        {
            return CurrentIteration.TechDebtImpactOnCapacity
                   + CurrentIteration.TotalCapacity
                   - ProposedForIterationCardList.Cost;
        }


        public bool MoveCard(IGameCardModel cardModel, GameCardListModel listToMoveTo)
        {
            if (!CanMoveCard(cardModel, listToMoveTo)) return false;

            ProposedForIterationCardList.Remove(cardModel);
            OutstandingFeaturesCardList.Remove(cardModel);
            OutstandingTechDebtCardList.Remove(cardModel);

            listToMoveTo.Add(cardModel);

            UpdateCurrentIteration();

            return true;
        }

        public bool CanMoveCard(IGameCardModel cardModel, GameCardListModel listToMoveTo)
        {
            if (!listToMoveTo.WillAccept(cardModel))
                return false;

            if (!HasCapacityForWork(cardModel, listToMoveTo))
                return false;

            return true;
        }

        public bool HasCapacityForWork(IGameCardModel cardModel, GameCardListModel list)
        {
            return list != ProposedForIterationCardList || CalculateAvailableCapacity() - cardModel.Cost >= 0;
        }
    }
}