namespace TechDebtGame.Model
{
    public interface IGameCardModel
    {
        string Scenario { get; set; }
        int Cost { get; set; }
    }

    public class GameCardModel : IGameCardModel
    {
        public string Scenario { get; set; }
        public int Cost { get; set; }
    }
}