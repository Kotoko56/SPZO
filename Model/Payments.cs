namespace SPZO.Model
{
    public class Payments
    {
        public int PaymentID { get; set; }
        public int UserID { get; set; }
        public string? PaymentType { get; set; }
        public int BeeHiveNumber { get; set; }
        public decimal SumOfPayment { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
