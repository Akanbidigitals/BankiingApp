using System.Globalization;

namespace BankingAPI.Models.DTOs
{
    public class DepositMoneyDTO
    {
        public string AcctNumber { get; set; }  

        public decimal Amount { get; set; }
    }
}
