using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOrderProductReviewController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UserOrderProductReviewController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetPendingReview(string userId)
        {
            var getUserPendingOrderReviews = await _dataContext.OrderProductReviews
                .Where(x => x.UserId == userId)
                .ToListAsync();
            return Ok();
        }
    }
}
