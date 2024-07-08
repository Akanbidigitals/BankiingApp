namespace BankingAPI.DataAcess.Utilities
{
    public class GenerateRandomActt
    {
        public static string GetAcctNumber()
        {
            Random random = new Random();
            string acctNumberDigit = "";

            for(int i=0; i < 10; i++)
            {
                acctNumberDigit += random.Next(0, 10).ToString();
            }
            return acctNumberDigit;
        }
    }
}
