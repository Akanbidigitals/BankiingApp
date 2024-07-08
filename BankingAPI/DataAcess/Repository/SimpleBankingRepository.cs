using BankingAPI.DataAcess.DataContext;
using BankingAPI.DataAcess.Interface;
using BankingAPI.DataAcess.Utilities;
using BankingAPI.Models;
using BankingAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.DataAcess.Repository
{
    public class SimpleBankingRepository : ISimpleBankingRepository
    {
        private readonly SimpleBankingContext _database;
        public SimpleBankingRepository(SimpleBankingContext database)
        {
            _database = database;
        }
        public async Task<SimpleBanking> CreateNewAccount(CreateAccountDto new_acct)
        {
            try
            {
                var checkAcctAlreadyExist = await _database.SimplBankings.AnyAsync(x => x.AccountBalance == new_acct.AccountBalance);
                if (checkAcctAlreadyExist)
                {
                    throw new Exception("User already exist");
                }
                var newAcct = new SimpleBanking()
                {
                    AccountName = new_acct.AccountName,
                    Email = new_acct.Email,
                    AccountNumber = GenerateRandomActt.GetAcctNumber(),
                    AccountBalance = new_acct.AccountBalance,
                };
                await _database.SimplBankings.AddAsync(newAcct);
                await _database.SaveChangesAsync();
                return newAcct;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SimpleBanking> DepositMoney(decimal amount ,string acct)
        {
            try
            {
                var validateAcct = await ValidateAcct(acct);
                if(validateAcct == null)
                {
                    throw new Exception("Account number does not exist");
                }
                if(amount <= 0)
                {
                    throw new Exception("You cant deposit zero or negative amount");
                }
                validateAcct.AccountBalance += amount;
                _database.SimplBankings.Update(validateAcct);
               var result =  await _database.SaveChangesAsync();

                if( result < 1)
                {
                    throw new DbUpdateException("An error while updating database");
                }
                return validateAcct;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<GetAccountBalanceDTO> GetAccountBalance(string acct_number)
        {
            try 
              {
                var checkAcct = await ValidateAcct(acct_number);
                if(checkAcct == null)
                {
                    throw new Exception("Account number does exist");
                }
                var ActBalance = new GetAccountBalanceDTO()
                {
                    AccountBalance = checkAcct.AccountBalance
                };
                return ActBalance;
                  
             }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
         
        }

        public async Task<string> TranserMoney(CreateTransferMoneyDTO transfer_dto)
        {
            try
            { 
                var Sender = await ValidateAcct(transfer_dto.SenderAcct);
                var Reciever = await ValidateAcct(transfer_dto.RecieverAcct);
                if(Sender == null || Reciever == null)
                {
                    throw new Exception("Sender or Recievers acct does not exist");
                }
                if(transfer_dto.Amount <= 0)
                {
                    throw new Exception("You cant send 0 or negative amount");
                }
                if(Sender.AccountBalance < 100)
                {
                    throw new Exception("Amount cant be less than #100");
                }
                Sender.AccountBalance -= transfer_dto.Amount;

                Reciever.AccountBalance += transfer_dto.Amount;
                _database.SimplBankings.Update(Sender);
                _database.SimplBankings.Update(Reciever);
               var result =  await _database.SaveChangesAsync();
                if(result < 1)
                {
                    throw new DbUpdateException("An error occur while updating database");
                }


                return $"You have succesfully transfered #{transfer_dto.Amount} from {Sender.AccountName} to {Reciever.AccountName}.";


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        public async Task<string> WithdrawMoney(WithdrawMoneyDTO withdraw_dto)
        {
            try
            {
                var checkWithdrawalAcct = await ValidateAcct(withdraw_dto.AccountNumber);
                if(checkWithdrawalAcct == null)
                {
                    throw new Exception("Acct does not exist");
                }
                if(checkWithdrawalAcct.AccountBalance < 100)
                {
                    throw new Exception("Account balance cant be less than #100");
                }
                if (withdraw_dto.Amount <= 0)
                {
                    throw new Exception("you cannot withdraw 0 or negative amount");
                }
                
                checkWithdrawalAcct.AccountBalance -= withdraw_dto.Amount;
                _database.SimplBankings.Update(checkWithdrawalAcct);
                var result = await _database.SaveChangesAsync();
                if(result < 1)
                {
                    throw new DbUpdateException("An error occur while updating Database");

                }

                return $"Dear {checkWithdrawalAcct.AccountName} you have successfully withdraw {withdraw_dto.Amount} from your accout.\nYour new balance is {checkWithdrawalAcct.AccountBalance}.";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<SimpleBanking> ValidateAcct (string acct_number)
        {
            var credential = await _database.SimplBankings.FirstOrDefaultAsync(x => x.AccountNumber == acct_number);
            if(credential != null)
            {
                return credential;
            }   
            else
            {
                return null;
            }
        }
    }
}
