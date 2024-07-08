using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingAPI.Models
{
    public class SimpleBanking
    {
        [Key]
        public Guid Id {  get; set; }
        public string AccountName { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required, Length(10, 10)]

        public string AccountNumber { get; set; } = "";
        [Column(TypeName ="decimal(18,2)")]
        public decimal AccountBalance { get; set; } = decimal.Zero;
        
    }
}
