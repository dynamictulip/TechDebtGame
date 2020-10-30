using System.Linq;

namespace TechDebtGame.Model
{
    internal class OutstandingTechDebtCardList : GameCardListModel
    {
        public OutstandingTechDebtCardList()
        {
            Cards.AddRange(new[]
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

        public int TechDebtImpact => Cards.OfType<TechDebtGameCardModel>()
            .Sum(d => d.Impact);

        public override bool WillAccept(IGameCardModel cardModel)
        {
            return cardModel.GetType() == typeof(TechDebtGameCardModel);
        }
    }
}