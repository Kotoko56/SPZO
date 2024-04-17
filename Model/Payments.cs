namespace SPZO.Model
{
    class Payments(int paymentID, int userID, string paymentType, int beeHiveNumber, decimal sumOfPayment, DateTime paymentDate)
    {
        public int PaymentID { get; set; } = paymentID;
        public int UserID { get; set; } = userID;
        public string PaymentType { get; set; } = paymentType;
        public int BeeHiveNumber { get; set; } = beeHiveNumber;
        public decimal SumOfPayment { get; set; } = sumOfPayment;
        public DateTime PaymentDate { get; set; } = paymentDate;
    }
}
