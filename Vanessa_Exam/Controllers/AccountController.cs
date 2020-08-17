using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vanessa_Exam.Controllers.DBContext;
using Vanessa_Exam.Models;

namespace Vanessa_Exam.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly APISettings _configuration;
        private readonly APIDBContext _context;
        private Random _random = new Random();

        public AccountController(APISettings myConfiguration, APIDBContext db)
        {
            _configuration = myConfiguration;
            _context = db;
        }

        [HttpPost]
        [Route("CreateAccount")]
        public async Task<IActionResult> CreateAccount(CreateAccount account)
        {
            var AccountID = Convert.ToInt32(_random.Next(0, 9999).ToString("D4"));
            var response = "";
            try
            {
                var accountDetails = _context.Account.Where(x => x.UserName.ToUpper() == account.UserName.ToUpper()).FirstOrDefault();
                if(accountDetails == null)
                {
                    Account acc = new Account();
                    acc.UserName = account.UserName;
                    acc.InitialBalance = Math.Round(account.InitialBalance, 2);
                    acc.AccountID = AccountID;
                    _context.Account.Add(acc);
                    await _context.SaveChangesAsync();
                    response = "Account ID: " + AccountID;
                }
                else
                {
                    response = "Username already Exist!";
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message.ToString() });

            }
            return Ok(response);

        }
    }
}
