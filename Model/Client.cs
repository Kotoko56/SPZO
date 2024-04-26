using System.ComponentModel.DataAnnotations;

namespace SPZO.Model
{
    //Class Client
    public class Client
    {
        [Key]
        public int ClientID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Pesel { get; set; }
        public string? HomeAddress { get; set; }
        public string? VetNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RhdNumber { get; set; }
        public string? ArimrNumber { get; set; }
    }
}
