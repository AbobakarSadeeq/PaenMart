using AutoMapper;
using Business_Core.Entities.Product;
using Business_Core.IServices;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModel.ProductViewModel;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly DataContext _dataContext;
        public ProductController(IMapper mapper, IProductService productService, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;

            _productService = productService;
        }

        [HttpPost]

        public async Task<IActionResult> AddProduct([FromForm] AddProductViewModel viewModel)
        {
            var convertingModel = _mapper.Map<Product>(viewModel);
            await _productService.InsertProduct(convertingModel, viewModel.File);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductViewModel viewModel)
        {
            var newData = _mapper.Map<Product>(viewModel);
            var oldData = await _productService.GetProduct(newData.ProductID);
            await _productService.UpdateProduct(oldData, newData);

            // if addedd more new images then 
            if (viewModel.File != null)
            {
                _productService.UpdateProductImages(viewModel.ProductID, viewModel.File);
            }

            return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteProduct(int Id)
        {
            _productService.DeleteProductData(Id);
            // Or
            //var findingData = await _productService.GetProduct(Id);
            //await _productService.DeleteProduct(findingData);
            //return Ok();
            return Ok();
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProduct(int Id)
        {
            var detailData = await _productService.GetSingleProduct(Id);
            if (detailData.OnDiscount)
            {
                var filteringLiveDiscountDeal2 = await _dataContext.ProductDiscountDeals.Include(a => a.DiscountDeal)
                    .Where(a => a.ProductId == detailData.ProductID &&
                a.DiscountDeal.DealStatus == "Live").FirstOrDefaultAsync();

                detailData.AfterDiscountPrice = filteringLiveDiscountDeal2.ProductAfterDiscountPrice;
                detailData.DiscountPercentage = filteringLiveDiscountDeal2.ProductPercentage;
                detailData.DiscountExpireAt = filteringLiveDiscountDeal2.DiscountDeal.DealExpireAt;
            }


            detailData.ShowStarsByRatings = (double)detailData.TotalProductStars / (detailData.Raiting * 5);
            detailData.ShowStarsByRatings = detailData.ShowStarsByRatings * 5;

            if (detailData.ShowStarsByRatings >= 0.3 && detailData.ShowStarsByRatings <= 0.7 ||  // 0.3 => 0.7 == 0.5
                detailData.ShowStarsByRatings >= 1.3 && detailData.ShowStarsByRatings <= 1.7 ||
                detailData.ShowStarsByRatings >= 2.3 && detailData.ShowStarsByRatings <= 2.7 ||
                detailData.ShowStarsByRatings >= 3.3 && detailData.ShowStarsByRatings <= 3.7 ||
                detailData.ShowStarsByRatings >= 4.3 && detailData.ShowStarsByRatings <= 4.7
                )
            {
                detailData.ShowStarsByRatings = Math.Ceiling(detailData.ShowStarsByRatings) - 0.5;


            }
            else if (detailData.ShowStarsByRatings >= 0 && detailData.ShowStarsByRatings <= 0.2 || // 0 => 0.2 == 0
               detailData.ShowStarsByRatings >= 1 && detailData.ShowStarsByRatings <= 1.2 ||
               detailData.ShowStarsByRatings >= 2 && detailData.ShowStarsByRatings <= 2.2 ||
               detailData.ShowStarsByRatings >= 3 && detailData.ShowStarsByRatings <= 3.2 ||
               detailData.ShowStarsByRatings >= 4 && detailData.ShowStarsByRatings <= 4.2
               )
            {
                detailData.ShowStarsByRatings = Math.Round(detailData.ShowStarsByRatings);


            }
            else if (detailData.ShowStarsByRatings > 0.7                           // 0.8 => 1 == 1
               || detailData.ShowStarsByRatings > 1.7 || detailData.ShowStarsByRatings > 2.7
               || detailData.ShowStarsByRatings > 3.7 || detailData.ShowStarsByRatings > 4.7)
            {
                detailData.ShowStarsByRatings = Math.Ceiling(detailData.ShowStarsByRatings);
            }
            return Ok(detailData);
        }


        // getAll products with its brand and with its nest-sub-category to show in list and show that data only in admin getproducts table.
        [HttpGet("GetSelectedCategoryProducts")]
        public async Task<IActionResult> GetSelectedCategoryProducts([FromQuery] PageSelectedAndNestCategoryId nestCategoryIdAndPageSelected)
        {
            var productList = await _productService.GetProducts(nestCategoryIdAndPageSelected);
            var convertProductData = _mapper.Map<List<GetProductForAdminViewModel>>(productList);
            int countProductsRows = nestCategoryIdAndPageSelected.singleCategoryTotalProductsCount;
            return Ok(new { productData = convertProductData, countProducts = countProductsRows });
        }

        // get the products by brand, when clicked on brand then get those whose are related.
        [HttpGet("GetProductsByBrand")]
        public async Task<IActionResult> GetProductsByBrand([FromQuery] PageSelectedAndNestCategoryId productByBrands)
        {
            var detailData = await _productService.GetProductsByBrandId(productByBrands);

            foreach (var item in detailData)
            {
                if (item.OnDiscount)
                {
                    var filteringLiveDiscountDeal2 = await _dataContext.ProductDiscountDeals.Include(a => a.DiscountDeal)
                        .Where(a => a.ProductId == item.ProductID &&
                    a.DiscountDeal.DealStatus == "Live").FirstOrDefaultAsync();

                    item.AfterDiscountPrice = filteringLiveDiscountDeal2.ProductAfterDiscountPrice;
                    item.DiscountPercentage = filteringLiveDiscountDeal2.ProductPercentage;
                }
            }


            var convertProductData = _mapper.Map<List<GetProductViewModel>>(detailData);
            foreach (var singleProduct in convertProductData)
            {
                singleProduct.ShowStarsByRatings = (double)singleProduct.TotalProductStars / (singleProduct.Raiting * 5);
                singleProduct.ShowStarsByRatings = singleProduct.ShowStarsByRatings * 5;

                if (singleProduct.ShowStarsByRatings >= 0.3 && singleProduct.ShowStarsByRatings <= 0.7 ||  // 0.3 => 0.7 == 0.5
                    singleProduct.ShowStarsByRatings >= 1.3 && singleProduct.ShowStarsByRatings <= 1.7 ||
                    singleProduct.ShowStarsByRatings >= 2.3 && singleProduct.ShowStarsByRatings <= 2.7 ||
                    singleProduct.ShowStarsByRatings >= 3.3 && singleProduct.ShowStarsByRatings <= 3.7 ||
                    singleProduct.ShowStarsByRatings >= 4.3 && singleProduct.ShowStarsByRatings <= 4.7
                    )
                {
                    singleProduct.ShowStarsByRatings = Math.Ceiling(singleProduct.ShowStarsByRatings) - 0.5;


                }
                else if (singleProduct.ShowStarsByRatings >= 0 && singleProduct.ShowStarsByRatings <= 0.2 || // 0 => 0.2 == 0
                   singleProduct.ShowStarsByRatings >= 1 && singleProduct.ShowStarsByRatings <= 1.2 ||
                   singleProduct.ShowStarsByRatings >= 2 && singleProduct.ShowStarsByRatings <= 2.2 ||
                   singleProduct.ShowStarsByRatings >= 3 && singleProduct.ShowStarsByRatings <= 3.2 ||
                   singleProduct.ShowStarsByRatings >= 4 && singleProduct.ShowStarsByRatings <= 4.2
                   )
                {
                    singleProduct.ShowStarsByRatings = Math.Round(singleProduct.ShowStarsByRatings);


                }
                else if (singleProduct.ShowStarsByRatings > 0.7                           // 0.8 => 1 == 1
                   || singleProduct.ShowStarsByRatings > 1.7 || singleProduct.ShowStarsByRatings > 2.7
                   || singleProduct.ShowStarsByRatings > 3.7 || singleProduct.ShowStarsByRatings > 4.7)
                {
                    singleProduct.ShowStarsByRatings = Math.Ceiling(singleProduct.ShowStarsByRatings);
                }
            }
            int countProductsRows = productByBrands.singleCategoryTotalProductsCount;
            return Ok(new { productData = convertProductData, countProducts = countProductsRows });
        }

        // get the product by nest category, when clicked on category then get those whose are related.
        [HttpGet("GetProductsByNestSubCategory")]
        public async Task<IActionResult> GetProductsByNestSubCategory([FromQuery] PageSelectedAndNestCategoryId pageSelectedAndNestCategoryId)
        {

            var detailData = await _productService.GetProductsByNestSubCategoryId(pageSelectedAndNestCategoryId);

            // loop on list 
            // if onDiscount product find then
            foreach (var item in detailData)
            {
                if (item.OnDiscount)
                {
                    var filteringLiveDiscountDeal2 = await _dataContext.ProductDiscountDeals.Include(a => a.DiscountDeal)
                        .Where(a => a.ProductId == item.ProductID &&
                    a.DiscountDeal.DealStatus == "Live").FirstOrDefaultAsync();

                    item.AfterDiscountPrice = filteringLiveDiscountDeal2.ProductAfterDiscountPrice;
                    item.DiscountPercentage = filteringLiveDiscountDeal2.ProductPercentage;
                }
            }


            var convertProductData = _mapper.Map<List<GetProductViewModel>>(detailData);
            foreach (var singleProduct in convertProductData)
            {
                singleProduct.ShowStarsByRatings = (double)singleProduct.TotalProductStars / (singleProduct.Raiting * 5);
                singleProduct.ShowStarsByRatings = singleProduct.ShowStarsByRatings * 5;

                if (singleProduct.ShowStarsByRatings >= 0.3 && singleProduct.ShowStarsByRatings <= 0.7 ||  // 0.3 => 0.7 == 0.5
                    singleProduct.ShowStarsByRatings >= 1.3 && singleProduct.ShowStarsByRatings <= 1.7 ||
                    singleProduct.ShowStarsByRatings >= 2.3 && singleProduct.ShowStarsByRatings <= 2.7 ||
                    singleProduct.ShowStarsByRatings >= 3.3 && singleProduct.ShowStarsByRatings <= 3.7 ||
                    singleProduct.ShowStarsByRatings >= 4.3 && singleProduct.ShowStarsByRatings <= 4.7
                    )
                {
                    singleProduct.ShowStarsByRatings = Math.Ceiling(singleProduct.ShowStarsByRatings) - 0.5;


                }
                else if (singleProduct.ShowStarsByRatings >= 0 && singleProduct.ShowStarsByRatings <= 0.2 || // 0 => 0.2 == 0
                   singleProduct.ShowStarsByRatings >= 1 && singleProduct.ShowStarsByRatings <= 1.2 ||
                   singleProduct.ShowStarsByRatings >= 2 && singleProduct.ShowStarsByRatings <= 2.2 ||
                   singleProduct.ShowStarsByRatings >= 3 && singleProduct.ShowStarsByRatings <= 3.2 ||
                   singleProduct.ShowStarsByRatings >= 4 && singleProduct.ShowStarsByRatings <= 4.2
                   )
                {
                    singleProduct.ShowStarsByRatings = Math.Round(singleProduct.ShowStarsByRatings);


                }
                else if (singleProduct.ShowStarsByRatings > 0.7                           // 0.8 => 1 == 1
                   || singleProduct.ShowStarsByRatings > 1.7 || singleProduct.ShowStarsByRatings > 2.7
                   || singleProduct.ShowStarsByRatings > 3.7 || singleProduct.ShowStarsByRatings > 4.7)
                {
                    singleProduct.ShowStarsByRatings = Math.Ceiling(singleProduct.ShowStarsByRatings);
                }
            }
            int countProductsRows = pageSelectedAndNestCategoryId.singleCategoryTotalProductsCount;
            return Ok(new { productData = convertProductData, countProducts = countProductsRows });
        }


        [HttpDelete("DeleteSingleProductSingleImage/{ImageId}")]
        public IActionResult DeleteSingleProductSingleImage(string ImageId)
        {
            _productService.DeletingSingleImageProduct(ImageId);
            return Ok();
        }

        // get only five products that most sell-out
        [HttpGet("GetMostSellFiveProducts")]
        public async Task<IActionResult> GetMostSellFiveProducts()
        {
            var get5MostSellProducts = await _productService.GetFiveMostSelledProducts();
            var convertProductData = _mapper.Map<List<GetProductViewModel>>(get5MostSellProducts);
            foreach (var singleProduct in convertProductData)
            {

                var filteringLiveDiscountDeal2 = await _dataContext.ProductDiscountDeals.Include(a => a.DiscountDeal)
                        .Where(a => a.ProductId == singleProduct.ProductID &&
                    a.DiscountDeal.DealStatus == "Live").FirstOrDefaultAsync();

                singleProduct.AfterDiscountPrice = filteringLiveDiscountDeal2 == null ? 0 : filteringLiveDiscountDeal2.ProductAfterDiscountPrice;
                singleProduct.DiscountPercentage = filteringLiveDiscountDeal2 == null ? 0 : filteringLiveDiscountDeal2.ProductPercentage;

                singleProduct.ShowStarsByRatings = (double)singleProduct.TotalProductStars / (singleProduct.Raiting * 5);
                singleProduct.ShowStarsByRatings = singleProduct.ShowStarsByRatings * 5;

                if (singleProduct.ShowStarsByRatings >= 0.3 && singleProduct.ShowStarsByRatings <= 0.7 ||  // 0.3 => 0.7 == 0.5
                    singleProduct.ShowStarsByRatings >= 1.3 && singleProduct.ShowStarsByRatings <= 1.7 ||
                    singleProduct.ShowStarsByRatings >= 2.3 && singleProduct.ShowStarsByRatings <= 2.7 ||
                    singleProduct.ShowStarsByRatings >= 3.3 && singleProduct.ShowStarsByRatings <= 3.7 ||
                    singleProduct.ShowStarsByRatings >= 4.3 && singleProduct.ShowStarsByRatings <= 4.7
                    )
                {
                    singleProduct.ShowStarsByRatings = Math.Ceiling(singleProduct.ShowStarsByRatings) - 0.5;


                }
                else if (singleProduct.ShowStarsByRatings >= 0 && singleProduct.ShowStarsByRatings <= 0.2 || // 0 => 0.2 == 0
                   singleProduct.ShowStarsByRatings >= 1 && singleProduct.ShowStarsByRatings <= 1.2 ||
                   singleProduct.ShowStarsByRatings >= 2 && singleProduct.ShowStarsByRatings <= 2.2 ||
                   singleProduct.ShowStarsByRatings >= 3 && singleProduct.ShowStarsByRatings <= 3.2 ||
                   singleProduct.ShowStarsByRatings >= 4 && singleProduct.ShowStarsByRatings <= 4.2
                   )
                {
                    singleProduct.ShowStarsByRatings = Math.Round(singleProduct.ShowStarsByRatings);


                }
                else if (singleProduct.ShowStarsByRatings > 0.7                           // 0.8 => 1 == 1
                   || singleProduct.ShowStarsByRatings > 1.7 || singleProduct.ShowStarsByRatings > 2.7
                   || singleProduct.ShowStarsByRatings > 3.7 || singleProduct.ShowStarsByRatings > 4.7)
                {
                    singleProduct.ShowStarsByRatings = Math.Ceiling(singleProduct.ShowStarsByRatings);
                }
            }
            return Ok(convertProductData);
        }

   

    }
}

