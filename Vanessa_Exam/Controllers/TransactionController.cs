using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vanessa_Exam.Controllers.DBContext;
using Vanessa_Exam.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vanessa_Exam.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly APISettings _configuration;
        private readonly APIDBContext _context;

        public TransactionController(APISettings myConfiguration, APIDBContext db)
        {
            _configuration = myConfiguration;
            _context = db;
        }

        [HttpPost]
        [Route("TransferMoney")]
        public async Task<IActionResult> TransferMoney(TransferMoney transfer)
        {
            try
            {
                var Account = _context.Account.ToList();
                var BalanceAmount = _context.Account.Where(x => x.AccountID == transfer.SourceAccountID).Select(a => a.InitialBalance).FirstOrDefault();
                var ExistingAccount = _context.Account.Where(a => a.AccountID == transfer.SourceAccountID).FirstOrDefault();
                var TransferAccount = _context.Account.Where(a => a.AccountID == transfer.DestinationAccountID).FirstOrDefault();
                var response = "";
                Guid TransactionID = Guid.NewGuid();

                if (ExistingAccount != null && TransferAccount != null)
                {
                    if(transfer.Amount == 0)
                    {
                        response = "No amount to transfer!";
                    }
                    else
                    {
                        foreach (var accountDetails in Account)
                        {
                            if (transfer.Amount > BalanceAmount)
                            {
                                response = "Insufficient Balance!";
                            }
                            else
                            {
                                if(ExistingAccount.AccountID == accountDetails.AccountID)
                                {
                                    var RemainingBalance = BalanceAmount - transfer.Amount;
                                    ExistingAccount.InitialBalance = RemainingBalance;
                                    _context.Entry(ExistingAccount).State = EntityState.Modified;
                                    await _context.SaveChangesAsync();
                                }

                                if (TransferAccount.AccountID == accountDetails.AccountID)
                                {
                                    var NewBalance = transfer.Amount + TransferAccount.InitialBalance;
                                    TransferAccount.InitialBalance = NewBalance;
                                    _context.Entry(TransferAccount).State = EntityState.Modified;
                                    await _context.SaveChangesAsync();
                                }

                                response = "Transaction ID: " + TransactionID;
                            }
                        }
                    }
                }
                else
                {
                    if (ExistingAccount == null && TransferAccount == null)
                    {
                        response = "Source and Destination Account ID does not exist!";
                    }
                    if(ExistingAccount == null)
                    {
                        response = "Source Account ID does not exist!";
                    }
                    if (TransferAccount == null)
                    {
                        response = "Destination Account ID does not exist!";
                    }
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message.ToString()
                });

            }
        }
    }
}
