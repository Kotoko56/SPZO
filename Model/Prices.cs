namespace SPZO.Model
{
    public class Prices
    {
        public decimal BeeHouse { get; set; }
        public decimal Membership { get; set; }
        public decimal Entry_fee { get; set; }
        public decimal Insurance { get; set; }

        public Prices(decimal beeHouse, decimal membership, decimal entry_fee, decimal insurance)
        {
            BeeHouse = beeHouse;
            Membership = membership;
            Entry_fee = entry_fee;
            Insurance = insurance;
        }

    }
}
