using System;
using System.Collections.Generic;
using System.Linq;
using TechDebtGame.Model;

namespace TechDebtGame.Pages
{
    public class GameManager
    {
        private const int TeamTotalCapacity = 60;

        private readonly OutstandingFeaturesCardList _outstandingFeaturesCardList = new OutstandingFeaturesCardList();
        private readonly OutstandingTechDebtCardList _outstandingTechDebtCardList = new OutstandingTechDebtCardList();
        private readonly ProposedForIterationCardList _proposedForIterationCardList = new ProposedForIterationCardList();
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
            _proposedForIterationCardList.Clear();

            if (Iterations.Any())
                _outstandingTechDebtCardList.Add(CurrentIteration.GameCardModel);

            var currentTechDebt = _outstandingTechDebtCardList.Cards.OfType<TechDebtGameCardModel>()
                .Sum(d => d.Impact);

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
            var costOfCurrentSprint = _proposedForIterationCardList.Cost;
            var featurePoints = _proposedForIterationCardList
                .Cards.Where(c => c.GetType() == typeof(GameCardModel))
                .Sum(f => f.Cost);

            CurrentIteration.AvailableCapacity = CurrentIteration.TechDebtImpactOnCapacity +
                CurrentIteration.TotalCapacity - costOfCurrentSprint;
            CurrentIteration.FeaturePointsComplete = featurePoints;
        }
        

        public void MoveCard(IGameCardModel cardModel, GameCardListType moveToList)
        {
            _proposedForIterationCardList.Remove(cardModel);
            _outstandingFeaturesCardList.Remove(cardModel);
            _outstandingTechDebtCardList.Remove(cardModel);

            GetCardList(moveToList).Add(cardModel);

            UpdateCurrentIteration();
        }

        private GameCardListModel GetCardList(GameCardListType moveToList)
        {
            switch (moveToList)
            {
                case GameCardListType.ProposedForIteration:
                    return _proposedForIterationCardList;

                case GameCardListType.OutstandingTechDebt:
                    return _outstandingTechDebtCardList;

                case GameCardListType.OutstandingFeatures:
                    return _outstandingFeaturesCardList;

                default:
                    throw new ArgumentOutOfRangeException(nameof(moveToList), moveToList, null);
            }
        }

        public List<IGameCardModel> GetCards(GameCardListType gameCardListType)
        {
            return GetCardList(gameCardListType).Cards;
        }
    }
}