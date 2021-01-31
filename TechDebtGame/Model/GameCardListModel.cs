using System.Collections.Generic;
using System.Linq;

namespace TechDebtGame.Model
{
    public abstract class GameCardListModel
    {
        private List<IGameCardModel> _cards = new List<IGameCardModel>();

        /// <summary>
        /// This property cannot be used to modify the contents of the Cards list
        /// Consider this property as entirely readonly
        /// </summary>
        public IEnumerable<IGameCardModel> Cards => _cards.ToArray();

        public int Cost => Cards.Sum(c => c.Cost);

        public void Clear()
        {
            _cards.Clear();
        }

        public void Add(IGameCardModel card)
        {
            _cards.Add(card);
            _cards = Cards.OrderBy(c => c.Cost).ThenBy(c => c.Scenario).ToList();
        }

        public void AddRange(IEnumerable<IGameCardModel> techDebtGameCardModels)
        {
            _cards.AddRange(techDebtGameCardModels);
            _cards = Cards.OrderBy(c => c.Cost).ThenBy(c => c.Scenario).ToList();
        }

        public void Remove(IGameCardModel card)
        {
            _cards.Remove(card);
        }

        public abstract bool WillAccept(IGameCardModel cardModel);
    }
}