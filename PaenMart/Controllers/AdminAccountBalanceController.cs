using AutoMapper;
using Business_Core.Entities.Identity.AdminAccount;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModel.IdentityViewModel.AdminAccountBalance;
using System.Diagnostics;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAccountBalanceController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public AdminAccountBalanceController(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentAccountStatus()
        {
            //var myStopWatch = Stopwatch.StartNew();
            //var getAccountDetails = await _dataContext.AdminAccounts
            //    .OrderByDescending(a => a.AdminAccountID)
            //    .FirstOrDefaultAsync();
            //myStopWatch.Stop();
            //var msOfWatchExecutionWithOrdering = myStopWatch.ElapsedMilliseconds;

        //    var myStopWatch2 = Stopwatch.StartNew();
            var getAccountSingleDetail = await _dataContext.AdminAccounts
                .ToListAsync();
            var lastAccountDetails = getAccountSingleDetail.LastOrDefault();
        //    myStopWatch2.Stop();
         //   var msOfWatchExecutionWithOrdering2 = myStopWatch2.ElapsedMilliseconds;


            return Ok(lastAccountDetails);
        }

        [HttpGet("GetAccountTransaction/{NumberTransaction}")]
        public async Task<IActionResult> GetAccountTransaction(int NumberTransaction)
        {
            //var myStopWatch = new Stopwatch();
            //myStopWatch.Start();
            var transactionList = await _dataContext.AdminAccounts
                .OrderByDescending(a => a.AdminAccountID)
                .Take(NumberTransaction)
                .ToListAsync();
            //myStopWatch.Stop();
            //var msOfWatchExecutionWithOrdering = myStopWatch.ElapsedMilliseconds;

            //        var myStopWatch2 = new Stopwatch();
            //       myStopWatch2.Start();
            //var transactionListConvert = await _dataContext.AdminAccounts
            //    .ToListAsync();
            //var takeLastNumberTransaction = transactionListConvert
            //    .TakeLast(NumberTransaction)
            //    .OrderByDescending(a => a.AdminAccountID);
            //     myStopWatch2.Stop();
            //     var msOfWatchExecutionWithList = myStopWatch2.ElapsedMilliseconds;




            return Ok(transactionList);
        }

        [HttpPost]
        public async Task<IActionResult> AddAccountBalance(AddAdminAccountBalanceViewModel adminAccountViewModel)
        {
            adminAccountViewModel.TransactionDateTime = DateTime.Now;
            var convertViewModel = _mapper.Map<AdminAccount>(adminAccountViewModel);
            await _dataContext.AdminAccounts.AddAsync(convertViewModel);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

    }
}
