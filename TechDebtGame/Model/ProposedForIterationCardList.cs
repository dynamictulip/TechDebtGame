using System.Linq;

namespace TechDebtGame.Model
{
    public class ProposedForIterationCardList : GameCardListModel
    {
        public int FeaturePoints => Cards
            .Where(c => c.GetType() == typeof(GameCardModel))
            .Sum(f => f.Cost);

        public override bool WillAccept(IGameCardModel cardModel)
        {
            return true;
        }
    }
}