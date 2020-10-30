using System.Collections.Generic;
using System.Linq;

namespace TechDebtGame.Model
{
    public class GameCardListModel
    {
        public GameCardListModel()
        {
        }

        public GameCardListModel(IEnumerable<IGameCardModel> cards)
        {
            Cards.AddRange(cards);
        }
        
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
    }
}