using System;
using System.Collections.Generic;
using System.Linq;
using TechDebtGame.Model;
using TechDebtGame.Shared;

namespace TechDebtGame.Pages
{
    public class GameManager
    {
        private const int TeamTotalCapacity = 60;
        public List<GameCardModel> CardsSelectedForIteration = new List<GameCardModel>();
        public List<TechDebtGameCardModel> OutstandingTechDebt;

        public GameManager()
        {
            InitialiseTechDebt();
            InitialiseIterationCards();
            StartNewIteration();
        }

        public List<IterationModel> Iterations { get; } = new List<IterationModel>();
        public IterationModel CurrentIteration => Iterations.Last();
        public IterationModel LastIteration => Iterations.Count > 1 ? Iterations[^2] : IterationModel.Empty;
        public Stack<TechDebtGameCardModel> IterationCards { get; private set; }

        public void StartNewIteration()
        {
            if (Iterations.Any())
                OutstandingTechDebt.Add(CurrentIteration.GameCardModel);

            var currentTechDebt = OutstandingTechDebt.Sum(d => d.Impact);
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
            var costOfCurrentSprint = CardsSelectedForIteration.Sum(c => c.Cost);
            CurrentIteration.AvailableCapacity = CurrentIteration.TechDebtImpactOnCapacity +
                CurrentIteration.TotalCapacity - costOfCurrentSprint;
        }

        public void InitialiseTechDebt()
        {
            OutstandingTechDebt = new List<TechDebtGameCardModel>(new[]
                {
                    new TechDebtGameCardModel {Cost = 5, Impact = -5, Scenario = "Refactor core code"},
                    new TechDebtGameCardModel
                        {Cost = 5, Impact = -5, Scenario = "Setup continuous integration environment"},
                    new TechDebtGameCardModel {Cost = 10, Impact = -5, Scenario = "Automate deployment"},
                    new TechDebtGameCardModel {Cost = 15, Impact = -10, Scenario = "Automate Unit Tests"},
                    new TechDebtGameCardModel {Cost = 15, Impact = -10, Scenario = "Automate Acceptance Tests"}
                }
            );
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

        public void MoveCard(GameCardModel cardModel, GameCardListType moveToList)
        {
            switch (moveToList)
            {
                case GameCardListType.Iteration:

                    if (OutstandingTechDebt.Contains(cardModel))
                        OutstandingTechDebt.Remove(cardModel as TechDebtGameCardModel);

                    if (!CardsSelectedForIteration.Contains(cardModel))
                        CardsSelectedForIteration.Add(cardModel);
                    break;

                case GameCardListType.OutstandingTechDebt:
                    if (CardsSelectedForIteration.Contains(cardModel))
                        CardsSelectedForIteration.Remove(cardModel);

                    if (!OutstandingTechDebt.Contains(cardModel))
                        OutstandingTechDebt.Add(cardModel as TechDebtGameCardModel);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(moveToList), moveToList, null);
            }

            UpdateCurrentIteration();
        }

        public List<GameCardModel> GetCards(GameCardListType gameCardListType)
        {
            return gameCardListType switch
            {
                GameCardListType.Iteration => CardsSelectedForIteration.ToList(),
                GameCardListType.OutstandingTechDebt => OutstandingTechDebt.OfType<GameCardModel>().ToList(),
                _ => throw new ArgumentOutOfRangeException(nameof(gameCardListType), gameCardListType, null)
            };
        }
    }
}