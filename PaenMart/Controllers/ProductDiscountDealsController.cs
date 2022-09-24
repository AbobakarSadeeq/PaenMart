﻿using AutoMapper;
using Business_Core.Entities;
using Business_Core.Entities.Product;
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
        private readonly IMapper _mapper;

        public ProductDiscountDealsController(DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper;
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
                .ThenInclude(a=>a.Product).ThenInclude(a=>a.ProductImages)
                .Where(a => a.DiscountDealID == selectedDealId).FirstOrDefaultAsync();
            var detailList = new List<GetDiscountDealDetailViewModel>();
            foreach (var item in findingSelectedDealDetails.ProductDiscountDeals)
            {
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
                    Expire_At = findingSelectedDealDetails.DealExpireAt
                    
                });
            }


            return Ok(detailList);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDealProductsList(UpdateDiscountDealViewModel viewModel)
        {
            if(viewModel.UpdateProductDiscountDealList.Count > 0)
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



    }
}