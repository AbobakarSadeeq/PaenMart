using Business_Core.Entities.ProductWishlist;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModel.ProductWishListViewModel;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWishListController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ProductWishListController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetSingleUserWishList(string UserId)
        {
            var findingSingleUserAllWishList = await _dataContext.ProductWishlists
                .Include(a => a.CustomIdentity).
                ThenInclude(a => a.UserImages)
                .Include(a => a.Product)
                .ThenInclude(a => a.ProductImages)
                .Where(x => x.UserId == UserId).Select(a => new
                {
                    a.ProductId,
                    a.ProductWishlistID,
                    a.Created_At,
                    a.UserId,
                    FullName = a.CustomIdentity.FullName,
                    ProductName = a.Product.ProductName + " (" + a.Product.Color + ")",
                    ProductImageUrl = a.Product.ProductImages[0].URL,
                    ProductPrice = a.Product.Price
                }).ToListAsync();

            return Ok(findingSingleUserAllWishList);
        }


        [HttpPost]
        public async Task<IActionResult> AddProductToWishList(ProductWishlistViewModel wishlist)
        {
            wishlist.Created_At = DateTime.Now;
            var convertingViewModel = new ProductWishlist
            {
                Created_At = DateTime.Now,
                ProductId = wishlist.ProductId,
                UserId = wishlist.UserId,
            };
            await _dataContext.ProductWishlists.AddAsync(convertingViewModel);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("IsProductInWishListUser")]
        public async Task<IActionResult> IsProductInWishListUser(ProductWishlistViewModel wishlist)
        {
            var selectedProductInWishList = await _dataContext.ProductWishlists
                .FirstOrDefaultAsync(a => a.UserId == wishlist.UserId && a.ProductId == wishlist.ProductId);
            return Ok(selectedProductInWishList);
        }

        [HttpPost("DeleteSelectedProductFromUserWishlist")]
        public async Task<IActionResult> DeleteSelectedProductFromUserWishlist(ProductWishlistViewModel wishlist)
        {
            var selectedProductInWishList = await _dataContext.ProductWishlists
                .FirstOrDefaultAsync(a => a.UserId == wishlist.UserId && a.ProductId == wishlist.ProductId);
            _dataContext.Remove(selectedProductInWishList);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

    }
}
