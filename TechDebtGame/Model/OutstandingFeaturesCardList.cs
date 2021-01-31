namespace TechDebtGame.Model
{
    public class OutstandingFeaturesCardList : GameCardListModel
    {
        public OutstandingFeaturesCardList()
        {
            AddRange(new IGameCardModel[]
                {
                    new GameCardModel {Cost = 5},
                    new GameCardModel {Cost = 5},
                    new GameCardModel {Cost = 5},
                    new GameCardModel {Cost = 10},
                    new GameCardModel {Cost = 10},
                    new GameCardModel {Cost = 10},
                    new GameCardModel {Cost = 15},
                    new GameCardModel {Cost = 15},
                    new GameCardModel {Cost = 15}
                }
            );
        }

        public override bool WillAccept(IGameCardModel cardModel)
        {
            return cardModel.GetType() == typeof(GameCardModel);
        }
    }
}