namespace SPZO.Model
{
    class Payments
    {
        public int PaymentID { get; set; }
        public int UserID { get; set; }
        public string PaymentType { get; set; }
        public int BeeHiveNumber { get; set; }
        public decimal SumOfPayment { get; set; }
        public DateTime PaymentDate { get; set; }

        public Payments(int paymentID, int userID, string paymentType, int beeHiveNumber, decimal sumOfPayment, DateTime paymentDate)
        {
            PaymentID = paymentID;
            UserID = userID;
            PaymentType = paymentType;
            BeeHiveNumber = beeHiveNumber;
            SumOfPayment = sumOfPayment;
            PaymentDate = paymentDate;
        }

    }
}
