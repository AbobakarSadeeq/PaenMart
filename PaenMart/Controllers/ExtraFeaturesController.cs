using Business_Core.Entities.Identity.Email;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtraFeaturesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ExtraFeaturesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetOwnerEmailDetail()
        {
            var getEmailInfo = await _dataContext.SendingEmails.FirstOrDefaultAsync();
            return Ok(getEmailInfo);
        }

        [HttpPost]
        public async Task<IActionResult> PostOwnerEmailDetail(SendingEmail sendingEmail)
        {
            await _dataContext.SendingEmails.AddAsync(sendingEmail);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOwnerEmailDetail(SendingEmail newData)
        {
            var findingEmailInfo = await _dataContext.SendingEmails.FirstOrDefaultAsync();
            findingEmailInfo.OwnerEmail = newData.OwnerEmail;
            findingEmailInfo.AppPassword = newData.AppPassword;
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        // ------------------------- Dashboard -------------------------

        [HttpGet("DashboardData")]
        public async Task<IActionResult> DashboardData()
        {
            var getProductsAndSumTotalWorth = await _dataContext.Products
                .SumAsync(a => a.Price);

            var totalUsers = await _dataContext.Users.CountAsync();

            var totalOrdersShipped = await _dataContext.Orders
                .Where(a=>a.OrderStatus == "Shipped")
                .CountAsync();

            var totalOrdersPending = await _dataContext.Orders
                .Where(a => a.OrderStatus == "Pending")
                .CountAsync();

            var getAccountTransaction = await _dataContext.AdminAccounts
                .OrderByDescending(a => a.AdminAccountID).FirstOrDefaultAsync();

            var numberOfProductsOnStock = await _dataContext.Products
                .Where(a => a.StockAvailiability == true)
                .CountAsync();

            return Ok(
                new
                {
                    ProductsWorth = getProductsAndSumTotalWorth, // done
                    totalUsersRegistered = totalUsers, // done
                    ShippedOrdersCount = totalOrdersShipped, // done
                    OrdersPendingCount = totalOrdersPending, // done
                    CurrentAccountBalanceOfAdmin = getAccountTransaction.CurrentBalance, // done
                    ProductInStockCount = numberOfProductsOnStock
                });
        }

        // Orders Chart Graph Data
        [HttpGet("OrdersChart")]
        public async Task<IActionResult> OrdersChart()
        {

            var monthtOrders = new List<int>();

            for (int month = 1; month <= 12; month++)
            {
                var shippedDataCount = await _dataContext.Orders
                    .Where(a => a.ShippedDate.Value.Date.Month == month).CountAsync();
                if (shippedDataCount == 0)
                {
                    monthtOrders.Add(0);
                }
                else
                {
                    monthtOrders.Add(shippedDataCount);
                }
            }

            return Ok(monthtOrders);
        }

        [HttpGet("GetLastFiveShippedOrders")]
        public async Task<IActionResult> GetLastFiveShippedOrders()
        {
            var getFiveOrdersShippedList = await _dataContext.Orders
                .OrderByDescending(a=>a.OrderID)
                .Where(a => a.OrderStatus == "Shipped")
                .Take(5)
                .ToListAsync();

            if (getFiveOrdersShippedList == null)
            {
                return Ok();
            }

            return Ok(getFiveOrdersShippedList);
        }


    }

}
