using System.ComponentModel.DataAnnotations;

namespace BankingAPI.Models.DTOs
{
    public class CreateAccountDto
    {
        public string AccountName { get; set; } = "";
       
        public string Email { get; set; } = "";
       
        public string AccountNumber { get; set; } = "";

        public decimal AccountBalance { get; set; } = decimal.Zero;


    }
}
