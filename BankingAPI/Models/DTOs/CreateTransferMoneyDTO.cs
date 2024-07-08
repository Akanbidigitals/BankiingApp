namespace BankingAPI.Models.DTOs
{
    public class CreateTransferMoneyDTO
    {
        public string SenderAcct { get; set; } = "";
        public string RecieverAcct { get; set; } = "";


        public decimal Amount { get; set; } = 0;   
    }
}
