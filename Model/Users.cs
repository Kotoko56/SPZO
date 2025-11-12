using System.ComponentModel.DataAnnotations;

namespace SPZO.Model
{
    public class Users
    {
        //Class Users
        [Key]
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
    }
}

    
