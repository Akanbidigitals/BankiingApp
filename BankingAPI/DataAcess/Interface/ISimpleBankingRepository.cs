using BankingAPI.Models.DTOs;
using BankingAPI.Models;

namespace BankingAPI.DataAcess.Interface
{
    public interface ISimpleBankingRepository
    {
        Task<SimpleBanking> CreateNewAccount(CreateAccountDto new_acct);
        Task<GetAccountBalanceDTO> GetAccountBalance(string acct_number);
        Task<SimpleBanking> DepositMoney(decimal amount, string accout);
        Task<string> TranserMoney(CreateTransferMoneyDTO new_acct);
        Task<string> WithdrawMoney(WithdrawMoneyDTO acct_number);
    }
}
