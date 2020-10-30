﻿namespace TechDebtGame.Model
{
    internal class OutstandingFeaturesCardList : GameCardListModel
    {
        public OutstandingFeaturesCardList()
        {
            Cards.AddRange(new IGameCardModel[]
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
    }
}