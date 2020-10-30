using System.Collections.Generic;
using System.Linq;
using Blazorise;

namespace TechDebtGame.Model
{
    public abstract class GameCardListModel
    {
        public List<IGameCardModel> Cards { get; } = new List<IGameCardModel>();
        public int Cost => Cards.Sum(c => c.Cost);

        public void Clear()
        {
            Cards.Clear();
        }

        public void Add(IGameCardModel card)
        {
            Cards.Add(card);
        }

        public void Remove(IGameCardModel card)
        {
            Cards.Remove(card);
        }

        public abstract bool WillAccept(IGameCardModel cardModel);
    }
}