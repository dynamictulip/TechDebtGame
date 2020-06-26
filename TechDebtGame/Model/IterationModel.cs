namespace TechDebtGame.Model
{
    public class IterationModel
    {
        public int Number { get; set; }
        public TechDebtGameCardModel GameCardModel { get; set; }
        public int TotalCapacity { get; set; }
        public int TechDebtImpactOnCapacity { get; set; }
        public int AvailableCapacity { get; set; }
        public int FeaturePointsComplete { get; set; }
        public static IterationModel Empty => new IterationModel { GameCardModel = new TechDebtGameCardModel()};
    }
}