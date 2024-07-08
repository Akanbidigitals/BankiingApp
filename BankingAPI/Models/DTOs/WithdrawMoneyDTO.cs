namespace BankingAPI.Models.DTOs
{
    public class WithdrawMoneyDTO
    {
        public string AccountNumber { get; set; } = "";

        public decimal Amount { get; set; }
    }
}
