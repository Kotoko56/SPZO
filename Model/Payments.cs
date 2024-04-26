using System.ComponentModel.DataAnnotations;

namespace SPZO.Model
{
    public class Payments
    {
        [Key]
        public int PaymentID { get; set; }
        public int ClientID { get; set; }
        public string? PaymentType { get; set; }
        public string? BeeHiveNumber { get; set; }
        public string? SumOfPayment { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
