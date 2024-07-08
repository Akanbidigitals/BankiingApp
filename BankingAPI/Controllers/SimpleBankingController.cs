using BankingAPI.DataAcess.DataContext;
using BankingAPI.DataAcess.Interface;
using BankingAPI.Models;
using BankingAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleBankingController : ControllerBase
    {
        private readonly ISimpleBankingRepository _repository;
        public SimpleBankingController(ISimpleBankingRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("CreateNewAccount")]
        public async Task<ActionResult<SimpleBanking>> CreateBankUser([FromBody] CreateAccountDto new_user)
        {
            try
            {
                var addAcct = new SimpleBanking()
                {
                    AccountName = new_user.AccountName,
                    Email = new_user.Email,
                    AccountNumber = new_user.AccountNumber,
                    AccountBalance = new_user.AccountBalance,
                };
                var newAcctAdded = await _repository.CreateNewAccount(new_user);
                return Ok(newAcctAdded);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("CheckAccoutBalance")]
        public async Task<ActionResult<SimpleBanking>> CheckAcctBalance(string acct_number)
        {
            try
            {
                var response = await _repository.GetAccountBalance(acct_number);
                if(response == null)
                {
                    return NotFound();
                }
                return Ok(response);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("DepositMoney")]
        public async Task<ActionResult<SimpleBanking>> DepositMoney(decimal amount,string acct_no)
        {
            
            try
            {
                var response = await _repository.DepositMoney(amount,acct_no);
                return Ok(response);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("TransferMoneyToAnotherPersonAcct")]

        public async Task<ActionResult<SimpleBanking>> TransferToOtherAcct([FromBody]CreateTransferMoneyDTO transfer_dto)
        {
            try
            {
                var response = await _repository.TranserMoney(transfer_dto);
                return Ok(response);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("WithdrawMoneyFromAccount")]
        public async Task<ActionResult<SimpleBanking>> WithdrawMoney([FromBody] WithdrawMoneyDTO withdrw_dto)
        {
            try
            {
                var response = await _repository.WithdrawMoney(withdrw_dto);
                return Ok(response);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
