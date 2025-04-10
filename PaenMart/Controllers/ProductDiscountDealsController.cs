using AutoMapper;
using Business_Core.Entities;
using Business_Core.Entities.Product;
using Data_Access.DataContext_Class;
using Hangfire;
using Hangfire.Common;
using Hangfire.Server;
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
        private readonly IMapper _mapper;

        public ProductDiscountDealsController(DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        [HttpGet("{nestSubCategoryId}")]
        public async Task<IActionResult> SelectedCategoriesProducts(int nestSubCategoryId)
        {
            var findingProductsByCategory = await _dataContext.Products.Include(a => a.ProductImages)
                .Where(a => a.NestSubCategoryId == nestSubCategoryId).Select(a => new
                {
                    ProductName = a.ProductName + " (" + a.Color + ")",
                    ProductImageUrl = a.ProductImages[0].URL,
                    ProductPrice = a.Price,
                    OnDiscount = a.OnDiscount,
                    ProductId = a.ProductID,
                }).ToListAsync();

            return Ok(findingProductsByCategory);
        }


        // Add Products to deal and also adding deals
        [HttpPost]
        public async Task<IActionResult> AddProductsInDiscountDeal(AddProductsInDiscountDealViewModel viewModel)
        {
            var listAdding = new List<ProductDiscountDeal>();
            var products = new List<Product>();
            foreach (var item in viewModel.SelectedProductsInDeal)
            {
                listAdding.Add(new ProductDiscountDeal
                {
                    ProductId = item.ProductId,
                    ProductAfterDiscountPrice = item.ProductAfterDiscountPrice,
                    ProductBeforePrice = item.ProductBeforePrice,
                    ProductPercentage = item.ProductPercentage,
                });

                var findingSelectedProduct = await _dataContext.Products.FirstOrDefaultAsync(a => a.ProductID == item.ProductId);
                findingSelectedProduct.OnDiscount = true;
                products.Add(findingSelectedProduct);
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
            _dataContext.Products.UpdateRange(products);
            await _dataContext.SaveChangesAsync();
            await SettingTheDelayCode(discountDeal.DiscountDealID);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetDiscountDeals()
        {
            var fetchingAllDiscountDeals = await _dataContext.DiscountDeals.Where(a => a.DealStatus == "Live").Select(a => new
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

        [HttpDelete("{selectedDiscountDealId}")]
        public async Task<IActionResult> DeleteDeal(int selectedDiscountDealId)
        {
            var productList = new List<Product>();

            var findingSelectedDealAllProducts = await _dataContext.ProductDiscountDeals
                .Where(a => a.DiscountDealId == selectedDiscountDealId).ToListAsync();

            foreach (var item in findingSelectedDealAllProducts)
            {
                var findingSelectedProduct = await _dataContext.Products.FirstOrDefaultAsync(a => a.ProductID == item.ProductId);
                findingSelectedProduct.OnDiscount = false;
                productList.Add(findingSelectedProduct);
            }

            var findingSelectedDeal = await _dataContext.DiscountDeals
                .FirstOrDefaultAsync(a => a.DiscountDealID == selectedDiscountDealId);
            _dataContext.DiscountDeals.Remove(findingSelectedDeal);
            _dataContext.Products.UpdateRange(productList);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("SelectedDealProductsDetail/{selectedDealId}")]
        public async Task<IActionResult> SelectedDealProductsDetail(int selectedDealId)
        {
            var findingSelectedDealDetails = await _dataContext.DiscountDeals.Include(a => a.ProductDiscountDeals)
                .ThenInclude(a => a.Product).ThenInclude(a => a.ProductImages)
                .Where(a => a.DiscountDealID == selectedDealId).FirstOrDefaultAsync();
            var detailList = new List<GetDiscountDealDetailViewModel>();
            foreach (var item in findingSelectedDealDetails.ProductDiscountDeals)
            {
                var showStarsByRatings = (double)item.Product.ProductTotalStars / (item.Product.Raitings * 5);
                showStarsByRatings = showStarsByRatings * 5;

                if (showStarsByRatings >= 0.3 && showStarsByRatings <= 0.7 ||  // 0.3 => 0.7 == 0.5
                    showStarsByRatings >= 1.3 && showStarsByRatings <= 1.7 ||
                    showStarsByRatings >= 2.3 && showStarsByRatings <= 2.7 ||
                    showStarsByRatings >= 3.3 && showStarsByRatings <= 3.7 ||
                    showStarsByRatings >= 4.3 && showStarsByRatings <= 4.7
                    )
                {
                    showStarsByRatings = Math.Ceiling(showStarsByRatings) - 0.5;


                }
                else if (showStarsByRatings >= 0 && showStarsByRatings <= 0.2 || // 0 => 0.2 == 0
                   showStarsByRatings >= 1 && showStarsByRatings <= 1.2 ||
                   showStarsByRatings >= 2 && showStarsByRatings <= 2.2 ||
                   showStarsByRatings >= 3 && showStarsByRatings <= 3.2 ||
                   showStarsByRatings >= 4 && showStarsByRatings <= 4.2
                   )
                {
                    showStarsByRatings = Math.Round(showStarsByRatings);


                }
                else if (showStarsByRatings > 0.7                           // 0.8 => 1 == 1
                   || showStarsByRatings > 1.7 || showStarsByRatings > 2.7
                   || showStarsByRatings > 3.7 || showStarsByRatings > 4.7)
                {
                    showStarsByRatings = Math.Ceiling(showStarsByRatings);
                }

                detailList.Add(new GetDiscountDealDetailViewModel
                {
                    ProductImageUrl = item.Product.ProductImages[0].URL,
                    DealName = findingSelectedDealDetails.DealName,
                    DiscountPercentage = item.ProductPercentage,
                    AfterPrice = item.ProductAfterDiscountPrice,
                    BeforePrice = item.ProductBeforePrice,
                    ProductName = item.Product.ProductName + " (" + item.Product.Color + ")",
                    ProductsLiveCount = findingSelectedDealDetails.ProductDiscountDeals.Count(),
                    ProductId = item.Product.ProductID,
                    Created_At = findingSelectedDealDetails.DealCreatedAt,
                    Expire_At = findingSelectedDealDetails.DealExpireAt,
                    ShowStarsByRatings = showStarsByRatings,
                    TotalProductStars = item.Product.ProductTotalStars,
                    Raiting = item.Product.Raitings
                });
            }


            return Ok(detailList);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDealProductsList(UpdateDiscountDealViewModel viewModel)
        {
            if (viewModel.UpdateProductDiscountDealList.Count > 0)
            {

                var productList = new List<Product>();

                foreach (var item in viewModel.UpdateProductDiscountDealList)
                {
                    var findingSelectedProduct = await _dataContext.Products.FirstOrDefaultAsync(a => a.ProductID == item.ProductId);
                    findingSelectedProduct.OnDiscount = true;
                    productList.Add(findingSelectedProduct);
                }

                // update the ondiscount property as well in product
                _dataContext.Products.UpdateRange(productList);

                // add products to product discount deal table.
                var convertingData = _mapper.Map<List<ProductDiscountDeal>>(viewModel.UpdateProductDiscountDealList);
                await _dataContext.ProductDiscountDeals.AddRangeAsync(convertingData);
            }


            // update the discountDeal table
            var findingDiscountDealSelectedObj = await _dataContext.DiscountDeals.FirstOrDefaultAsync(a => a.DiscountDealID == viewModel.DiscountDealId);
            findingDiscountDealSelectedObj.DealName = viewModel.DealName;
            findingDiscountDealSelectedObj.DealExpireAt = viewModel.ExpireAt;
            _dataContext.DiscountDeals.Update(findingDiscountDealSelectedObj);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("GetExpireDiscountDeal")]
        public async Task<IActionResult> GetExpireDiscountDeal()
        {
            var fetchingAllDiscountDeals = await _dataContext.DiscountDeals.Where(a => a.DealStatus == "Expire").Select(a => new
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

        [HttpPost("SelectedLocalStorageProducts")]
        public async Task<IActionResult> SelectedLocalStorageProducts(int[] productsId)
        {

           
            List<SearchingCartProductsInDeal> list = new List<SearchingCartProductsInDeal>();
            foreach (var singleCartProductId in productsId)
            {
                var findingProductInDeal = await _dataContext.Products.FirstOrDefaultAsync(a => a.ProductID == singleCartProductId);
                if (findingProductInDeal != null && findingProductInDeal.OnDiscount == true)
                {
                    // it means product is in still in deal r8 now
                    var filteringLiveDiscountDeal2 = await _dataContext.ProductDiscountDeals.Include(a => a.DiscountDeal)
                         .Where(a => a.ProductId == findingProductInDeal.ProductID &&
                     a.DiscountDeal.DealStatus == "Live").FirstOrDefaultAsync();


                    list.Add(new SearchingCartProductsInDeal
                    {
                        ProductId = findingProductInDeal.ProductID,
                        ProductInDeal = true,
                        AfterDiscountPrice = filteringLiveDiscountDeal2.ProductAfterDiscountPrice,
                        DiscountPercentage = filteringLiveDiscountDeal2.ProductPercentage
                    });

                }
                else
                {
                    list.Add(new SearchingCartProductsInDeal
                    {
                        ProductId = findingProductInDeal.ProductID,
                        ProductInDeal = false
                    });

                }

            }
            return Ok(list);
        }

        private async Task SettingTheDelayCode(int currentlyLiveDiscountId)
        {

            var getDiscountLive = await _dataContext.DiscountDeals.Where(a => a.DiscountDealID == currentlyLiveDiscountId)
                .FirstOrDefaultAsync(); // because first one is not yet on exipire thats why it didnt goto another to also expire it
            if (getDiscountLive != null)
            {
                DateTime startTime = DateTime.Now;
                var endTime = getDiscountLive.DealExpireAt.Value;

                TimeSpan span = endTime - startTime;
                var totalMinutesDifferences = span.TotalMinutes;

                BackgroundJob.Schedule(
         () => ExpiringDiscountDeal(getDiscountLive.DiscountDealID),
                  TimeSpan.FromMinutes(totalMinutesDifferences));


            }
        }
        private async Task ExpiringDiscountDeal(int discountDealId)
        {
            var productList = new List<Product>();
            var findingDeal = await _dataContext.DiscountDeals
                .Include(a => a.ProductDiscountDeals)
                .FirstOrDefaultAsync(a => a.DiscountDealID == discountDealId);
            if (findingDeal != null)
            {

                if (findingDeal.DealStatus != "Expire")
                {
                    findingDeal.DealStatus = "Expire";
                    _dataContext.DiscountDeals.UpdateRange(findingDeal);


                    foreach (var item in findingDeal.ProductDiscountDeals)
                    {
                        var findingSelectedProduct = await _dataContext.Products.FirstOrDefaultAsync(a => a.ProductID == item.ProductId);
                        findingSelectedProduct.OnDiscount = false;
                        productList.Add(findingSelectedProduct);
                    }

                    _dataContext.Products.UpdateRange(productList);
                    await _dataContext.SaveChangesAsync();
                }
            }

        }




    }
}
