using AutoMapper;
using Business_Core.Entities.Identity.AdminAccount;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModel.IdentityViewModel.AdminAccountBalance;

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
            var getAccountDetails = await _dataContext.AdminAccounts
                .OrderByDescending(a => a.AdminAccountID)
                .FirstOrDefaultAsync();
            return Ok(getAccountDetails);
        }

        [HttpGet("GetAccountTransaction/{NumberTransaction}")]
        public async Task<IActionResult> GetAccountTransaction(int NumberTransaction)
        {
            var transactionList = await _dataContext.AdminAccounts
                .OrderByDescending(a=>a.AdminAccountID)
                .Take(NumberTransaction)
                .ToListAsync();
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
