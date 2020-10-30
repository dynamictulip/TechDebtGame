using System;
using System.Collections.Generic;
using System.Linq;
using TechDebtGame.Model;

namespace TechDebtGame.Pages
{
    public class GameManager
    {
        private const int TeamTotalCapacity = 60;

        private readonly List <GameCardListModel> _cardLists = new List<GameCardListModel>();
        private OutstandingFeaturesCardList _outstandingFeaturesCardList = new OutstandingFeaturesCardList();
        private OutstandingTechDebtCardList _outstandingTechDebtCardList = new OutstandingTechDebtCardList();
        private ProposedForIterationCardList _proposedForIterationCardList = new ProposedForIterationCardList();

        public GameManager()
        {
            _cardLists.Add(_outstandingFeaturesCardList);
            _cardLists.Add(_outstandingTechDebtCardList);
            _cardLists.Add(_proposedForIterationCardList);

            InitialiseIterationCards();

            StartNewIteration();
        }
        
        public List<IterationModel> Iterations { get; } = new List<IterationModel>();
        public IterationModel CurrentIteration => Iterations.Last();
        public IterationModel LastIteration => Iterations.Count > 1 ? Iterations[^2] : IterationModel.Empty;
        public Stack<TechDebtGameCardModel> IterationCards { get; private set; }

        public void StartNewIteration()
        {
            _proposedForIterationCardList.Clear();

            if (Iterations.Any())
                _outstandingTechDebtCardList.Add(CurrentIteration.GameCardModel);

            var currentTechDebt = _outstandingTechDebtCardList.Cards.OfType<TechDebtGameCardModel>()
                .Sum(d => d.Impact);
            var availableCapacity = TeamTotalCapacity + currentTechDebt;
            var randomScenario = IterationCards.Pop();

            Iterations.Add(new IterationModel
            {
                Number = Iterations.Count + 1,
                GameCardModel = randomScenario,
                TotalCapacity = TeamTotalCapacity,
                TechDebtImpactOnCapacity = currentTechDebt,
                AvailableCapacity = availableCapacity
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
        
        public void InitialiseIterationCards()
        {
            IterationCards = new Stack<TechDebtGameCardModel>(new[]
            {
                new TechDebtGameCardModel
                {
                    Cost = 0, Impact = 0,
                    Scenario =
                        "The team implemented features cleanly, with good design and no direct tradeoffs. No new debt recorded!"
                },
                new TechDebtGameCardModel
                {
                    Cost = 10, Impact = -5,
                    Scenario =
                        "Database is getting large and complex. We need to add automated migrations to reduce manual work and support refactoring."
                },
                new TechDebtGameCardModel
                {
                    Cost = 15, Impact = -5,
                    Scenario =
                        "During testing new defects were found. This must be addressed as technical debt. If continuous integration is in place, you may disregard this card."
                },
                new TechDebtGameCardModel
                {
                    Cost = 10, Impact = -5,
                    Scenario =
                        "The VP of sales pushes the team on urgent delivery. They chose simpler implementation and now need come back later to improve scalability."
                },
                new TechDebtGameCardModel
                {
                    Cost = 5, Impact = -5,
                    Scenario =
                        "Issues were found in production and the team had to push out a quick patch. Code Debt was incurred."
                },
                new TechDebtGameCardModel
                {
                    Cost = 0, Impact = 0,
                    Scenario =
                        "The Product Owner will not agree to more than 25% Capacity dedicated to Tech Debt for the next iteration."
                },
                new TechDebtGameCardModel
                {
                    Cost = 15, Impact = -10,
                    Scenario =
                        "New Features must be added to a module, but it is tightly coupled to others, making implementation difficult."
                },
                new TechDebtGameCardModel
                {
                    Cost = 5, Impact = -5,
                    Scenario =
                        "A significant portion of a core class is now obsolete, making new development more difficult."
                },
                new TechDebtGameCardModel
                {
                    Cost = 10, Impact = -5,
                    Scenario =
                        "The Deployment process has become increasingly complex. If Deployment Automation has been completed, you may disregard this card."
                },
                new TechDebtGameCardModel
                {
                    Cost = 5, Impact = -5,
                    Scenario =
                        "The application is using an older version of a 3rd party library and it should be updated."
                }
            });

            IterationCards.Shuffle();
        }

        public void MoveCard(IGameCardModel cardModel, GameCardListType moveToList)
        {
            foreach (var list in _cardLists) list.Remove(cardModel);

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