using System.Collections.Generic;
using System.Linq;
using TechDebtGame.Model;

namespace TechDebtGame.Pages
{
    internal class IterationManager
    {
        private const int TeamTotalCapacity = 60;
        private Stack<IterationCard> iterationCards;
        public List<Iteration> iterations;

        public Iteration CurrentIteration => iterations.Last();
        public Iteration LastIteration => iterations.Count > 1 ? iterations[^2] : Iteration.Empty;

        public void InitialiseIterations()
        {
            var cards = new[]
            {
                new IterationCard
                {
                    TechDebtCard = new TechDebtCard {Cost = 0, Impact = 0},
                    Scenario =
                        "The team implemented features cleanly, with good design and no direct tradeoffs. No new debt recorded!"
                },
                new IterationCard
                {
                    TechDebtCard = new TechDebtCard {Cost = 10, Impact = -5},
                    Scenario =
                        "Database is getting large and complex. We need to add automated migrations to reduce manual work and support refactoring."
                },
                new IterationCard
                {
                    TechDebtCard = new TechDebtCard {Cost = 15, Impact = -5},
                    Scenario =
                        "During testing new defects were found. This must be addressed as technical debt. If continuous integration is in place, you may disregard this card."
                },
                new IterationCard
                {
                    TechDebtCard = new TechDebtCard {Cost = 10, Impact = -5},
                    Scenario =
                        "The VP of sales pushes the team on urgent delivery. They chose simpler implementation and now need come back later to improve scalability."
                },
                new IterationCard
                {
                    TechDebtCard = new TechDebtCard {Cost = 5, Impact = -5},
                    Scenario =
                        "Issues were found in production and the team had to push out a quick patch. Code Debt was incurred."
                },
                new IterationCard
                {
                    TechDebtCard = new TechDebtCard {Cost = 0, Impact = 0},
                    Scenario =
                        "The Product Owner will not agree to more than 25% Capacity dedicated to Tech Debt for the next iteration."
                },
                new IterationCard
                {
                    TechDebtCard = new TechDebtCard {Cost = 15, Impact = -10},
                    Scenario =
                        "New Features must be added to a module, but it is tightly coupled to others, making implementation difficult."
                },
                new IterationCard
                {
                    TechDebtCard = new TechDebtCard {Cost = 5, Impact = -5},
                    Scenario =
                        "A significant portion of a core class is now obsolete, making new development more difficult."
                },
                new IterationCard
                {
                    TechDebtCard = new TechDebtCard {Cost = 10, Impact = -5},
                    Scenario =
                        "The Deployment process has become increasingly complex. If Deployment Automation has been completed, you may disregard this card."
                },
                new IterationCard
                {
                    TechDebtCard = new TechDebtCard {Cost = 5, Impact = -5},
                    Scenario =
                        "The application is using an older version of a 3rd party library and it should be updated."
                }
            };

            iterationCards = new Stack<IterationCard>(cards);
            iterationCards.Shuffle();
            iterations = new List<Iteration>();
            StartNewIteration();
        }

        public void StartNewIteration()
        {
            var currentTechDebt = -35;
            var availableCapacity = TeamTotalCapacity - currentTechDebt;
            var randomScenario = iterationCards.Pop();

            iterations.Add(new Iteration
            {
                Number = iterations.Count + 1,
                Card = randomScenario,
                TotalCapacity = TeamTotalCapacity,
                TechDebtImpactOnCapacity = currentTechDebt,
                AvailableCapacity = availableCapacity
            });
        }
    }
}