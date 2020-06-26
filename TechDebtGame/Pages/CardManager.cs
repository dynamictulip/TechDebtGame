using System.Collections.Generic;
using TechDebtGame.Model;

namespace TechDebtGame.Pages
{
    public class CardManager
    {
        public List<TechDebtGameCard> OutstandingTechDebt;

        public CardManager()
        {
            InitialiseIterationCards();
            InitialiseTechDebt();
        }

        public Stack<TechDebtGameCard> IterationCards { get; private set; }

        public void InitialiseTechDebt()
        {
            OutstandingTechDebt = new List<TechDebtGameCard>(new[]
                {
                    new TechDebtGameCard {Cost = 15, Impact = -10, Scenario = "Automate Acceptance Tests"},
                    new TechDebtGameCard {Cost = 5, Impact = -5, Scenario = "Refactor core code"},
                    new TechDebtGameCard {Cost = 5, Impact = -5, Scenario = "Setup continuous integration environment"},
                    new TechDebtGameCard {Cost = 15, Impact = -10, Scenario = "Automate Unit Tests"},
                    new TechDebtGameCard {Cost = 10, Impact = -5, Scenario = "Automate deployment"}
                }
            );
        }


        public void InitialiseIterationCards()
        {
            IterationCards = new Stack<TechDebtGameCard>(new[]
            {
                new TechDebtGameCard
                {
                    Cost = 0, Impact = 0,
                    Scenario =
                        "The team implemented features cleanly, with good design and no direct tradeoffs. No new debt recorded!"
                },
                new TechDebtGameCard
                {
                    Cost = 10, Impact = -5,
                    Scenario =
                        "Database is getting large and complex. We need to add automated migrations to reduce manual work and support refactoring."
                },
                new TechDebtGameCard
                {
                    Cost = 15, Impact = -5,
                    Scenario =
                        "During testing new defects were found. This must be addressed as technical debt. If continuous integration is in place, you may disregard this card."
                },
                new TechDebtGameCard
                {
                    Cost = 10, Impact = -5,
                    Scenario =
                        "The VP of sales pushes the team on urgent delivery. They chose simpler implementation and now need come back later to improve scalability."
                },
                new TechDebtGameCard
                {
                    Cost = 5, Impact = -5,
                    Scenario =
                        "Issues were found in production and the team had to push out a quick patch. Code Debt was incurred."
                },
                new TechDebtGameCard
                {
                    Cost = 0, Impact = 0,
                    Scenario =
                        "The Product Owner will not agree to more than 25% Capacity dedicated to Tech Debt for the next iteration."
                },
                new TechDebtGameCard
                {
                    Cost = 15, Impact = -10,
                    Scenario =
                        "New Features must be added to a module, but it is tightly coupled to others, making implementation difficult."
                },
                new TechDebtGameCard
                {
                    Cost = 5, Impact = -5,
                    Scenario =
                        "A significant portion of a core class is now obsolete, making new development more difficult."
                },
                new TechDebtGameCard
                {
                    Cost = 10, Impact = -5,
                    Scenario =
                        "The Deployment process has become increasingly complex. If Deployment Automation has been completed, you may disregard this card."
                },
                new TechDebtGameCard
                {
                    Cost = 5, Impact = -5,
                    Scenario =
                        "The application is using an older version of a 3rd party library and it should be updated."
                }
            });

            IterationCards.Shuffle();
        }
    }
}