using Business_Core.Entities;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModel.ProductsInDiscountDealsViewModel;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDiscountDealsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ProductDiscountDealsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("{nestSubCategoryId}")]
        public async Task<IActionResult> SelectedCategoriesProducts(int nestSubCategoryId)
        {
            var findingProductsByCategory = await _dataContext.Products.Include(a=>a.ProductImages)
                .Where(a => a.NestSubCategoryId == nestSubCategoryId).Select(a => new
                {
                    ProductName = a.ProductName + " (" + a.Color + ")",
                    ProductImageUrl = a.ProductImages[0].URL,
                    ProductPrice = a.Price,
                    OnDiscount = a.OnDiscount,
                    ProductId = a.ProductID
                }).ToListAsync();
            // featuer filtering by onDiscount in client side

            return Ok(findingProductsByCategory);
        }


        // Add Products to deal and also adding deals
        [HttpPost]
        public async Task<IActionResult> AddProductsInDiscountDeal(AddProductsInDiscountDealViewModel viewModel)
        {
            var listAdding = new List<ProductDiscountDeal>();
            foreach (var item in viewModel.SelectedProductsInDeal)
            {
                listAdding.Add(new ProductDiscountDeal
                {
                    ProductId = item.ProductId,
                    ProductAfterDiscountPrice = item.ProductAfterDiscountPrice,
                    ProductBeforePrice = item.ProductBeforePrice,
                    ProductPercentage = item.ProductPercentage,
                    
                });
            }

            var discountDeal = new Business_Core.Entities.DiscountDeal.DiscountDeal
            {
                DealName = viewModel.DealName,
                DealStatus = "Live",
                DealCreatedAt = DateTime.Now,
                DealExpireAt = viewModel.DealExpireAt,
                ProductDiscountDeals = listAdding
            };
            await _dataContext.DiscountDeals.AddAsync(discountDeal);
            await _dataContext.SaveChangesAsync();
            
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetDiscountDeals()
        {
            var fetchingAllDiscountDeals = await _dataContext.DiscountDeals.Where(a=>a.DealStatus == "Live").Select(a=> new
            {
                CountProducts = a.ProductDiscountDeals.Count,
                a.DiscountDealID,
                a.DealStatus,
                a.DealCreatedAt,
                a.DealExpireAt,
                a.DealName,
            }).ToListAsync();
            return Ok(fetchingAllDiscountDeals);
        }


    }
}
