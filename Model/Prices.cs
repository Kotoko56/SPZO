namespace SPZO.Model
{
    public class Prices(decimal beeHouse, decimal membership, decimal entry_fee, decimal insurance)
    {
        public decimal BeeHouse { get; set; } = beeHouse;
        public decimal Membership { get; set; } = membership;
        public decimal Entry_fee { get; set; } = entry_fee;
        public decimal Insurance { get; set; } = insurance;

        Prices prices = new Prices(3m, 50m, 50m, 15m);
    }

}
