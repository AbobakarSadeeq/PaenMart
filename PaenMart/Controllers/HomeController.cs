using AutoMapper;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModel.OrderProductReview;
using Presentation.ViewModel.ProductViewModel;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly DataContext dataContext;
        private readonly IMapper _mapper;

        public HomeController(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetFiveProductsOnDiscountSale()
        {
            List<GetProductViewModel> convertProductData = new List<GetProductViewModel>();
            var gettingFiveProductsOnSale = await dataContext.Products.Include(a=>a.ProductBrand).Include(a=>a.ProductImages)
                .Where(a => a.OnDiscount == true).Take(5).ToListAsync();

            foreach (var item in gettingFiveProductsOnSale)
            {
                convertProductData.Add(new GetProductViewModel
                {
                    Price = item.Price,
                    ProductBrandId = item.ProductBrandId,
                    ProductBrandName = item.ProductBrand.BrandName,
                    Color = item.Color,
                    ImageUrl = item.ProductImages[0].URL,
                    ProductID = item.ProductID,
                    ProductName = item.ProductName,
                    Raiting = item.Raitings,
                    TotalProductStars = item.ProductTotalStars
                });
            }



            foreach (var item in convertProductData)
            {
                var filteringLiveDiscountDeal2 = await dataContext.ProductDiscountDeals.Include(a => a.DiscountDeal)
                        .Where(a => a.ProductId == item.ProductID &&
                    a.DiscountDeal.DealStatus == "Live").FirstOrDefaultAsync();

                item.AfterDiscountPrice = filteringLiveDiscountDeal2.ProductAfterDiscountPrice;
                item.DiscountPercentage = filteringLiveDiscountDeal2.ProductPercentage;

                item.ShowStarsByRatings = (double)item.TotalProductStars / (item.Raiting * 5);
                item.ShowStarsByRatings = item.ShowStarsByRatings * 5;

                if (item.ShowStarsByRatings >= 0.3 && item.ShowStarsByRatings <= 0.7 ||  // 0.3 => 0.7 == 0.5
                item.ShowStarsByRatings >= 1.3 && item.ShowStarsByRatings <= 1.7 ||
                item.ShowStarsByRatings >= 2.3 && item.ShowStarsByRatings <= 2.7 ||
                item.ShowStarsByRatings >= 3.3 && item.ShowStarsByRatings <= 3.7 ||
                item.ShowStarsByRatings >= 4.3 && item.ShowStarsByRatings <= 4.7
                      )
                {
                    item.ShowStarsByRatings = Math.Ceiling(item.ShowStarsByRatings) - 0.5;


                }
                else if (item.ShowStarsByRatings >= 0 && item.ShowStarsByRatings <= 0.2 || // 0 => 0.2 == 0
                   item.ShowStarsByRatings >= 1 && item.ShowStarsByRatings <= 1.2 ||
                   item.ShowStarsByRatings >= 2 && item.ShowStarsByRatings <= 2.2 ||
                   item.ShowStarsByRatings >= 3 && item.ShowStarsByRatings <= 3.2 ||
                   item.ShowStarsByRatings >= 4 && item.ShowStarsByRatings <= 4.2
                   )
                {
                    item.ShowStarsByRatings = Math.Round(item.ShowStarsByRatings);


                }
                else if (item.ShowStarsByRatings > 0.7                           // 0.8 => 1 == 1
                   || item.ShowStarsByRatings > 1.7 || item.ShowStarsByRatings > 2.7
                   || item.ShowStarsByRatings > 3.7 || item.ShowStarsByRatings > 4.7)
                {
                    item.ShowStarsByRatings = Math.Ceiling(item.ShowStarsByRatings);
                }
            }

            return Ok(convertProductData);




        }


        [HttpGet("GetFiveLatestProductReview")]
        public async Task<IActionResult> GetFiveLatestProductReview()
        {
            List<GetFiveProductsReviewViewModel> convertToViewModel = new List<GetFiveProductsReviewViewModel>();
            var lastFiveReviewsOfProducts = await dataContext.OrderProductReviews.Include(a => a.Product)
                .ThenInclude(a=>a.ProductImages)
                .Where(a=>a.ReviewStatus == "Reviewed")
                .OrderByDescending(a => a.OrderProductReviewID)
                .Take(5).ToListAsync();

            foreach (var item in lastFiveReviewsOfProducts)
            {
                convertToViewModel.Add(new GetFiveProductsReviewViewModel
                {
                    ProductId = item.ProductId,
                    ReviewDate = item.Add_Review_Date,
                    Comment = item.ProductComment,
                    ProductImageUrl = item.Product.ProductImages[0].URL,
                    ProductName = item.Product.ProductName + " (" + item.Product.Color + ")",
                    RaitingStars = item.RaitingStars
                });
            }






            return Ok(convertToViewModel);
        }


    }
}
