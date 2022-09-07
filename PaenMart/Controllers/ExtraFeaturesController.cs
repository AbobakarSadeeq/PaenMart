using Business_Core.Entities.Identity.Email;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModel.ProductViewModel;
using Presentation.ViewModel.SearchProductsViewModel;

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
        [Authorize(Roles = "Admin")]
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

        [HttpPost("AutoCompletionSearch")]
        public async Task<IActionResult> AutoCompletionSearch(SearchProductViewModel viewModel)
        {
            // first search by brandName and nest-category

            // finding by complete writing brand-category-name
            var findingSearchItemBrand = await _dataContext
              .ProductBrands.Where(a => a.BrandName.Contains(viewModel.SearchText))
              .Select(a => new
              {
                  a.BrandName
              })
              .ToListAsync();

            // finding by complete writing nest-category-name
            var findingSearchItemByNestCategory = await _dataContext
               .NestSubCategories.Where(a => a.NestSubCategoryName.Contains(viewModel.SearchText))
               .Select(a => new
               {
                   a.NestSubCategoryName
               }).ToListAsync();

           

          

            // if not fullfulling the count then search it by the product names

            var findingByProductName = await _dataContext.Products
                .Where(a => a.ProductName.Contains(viewModel.SearchText))
                .Take(10)
                .Select(a => new 
                {
                    a.ProductName
                }).ToListAsync();




        

            
            // found data in list
            var combiningTheSearchData = new List<string>();



            int i = 0;
            while(combiningTheSearchData.Count <= 10)
            {

                if(findingSearchItemBrand.Count - 1 >= i)
                {
                    combiningTheSearchData.Add(findingSearchItemBrand[i].BrandName);
                }

                if(findingSearchItemByNestCategory.Count - 1 >= i)
                {
                    combiningTheSearchData.Add(findingSearchItemByNestCategory[i].NestSubCategoryName);
                }

                if(findingByProductName.Count -1 >= i)
                {
                    combiningTheSearchData.Add(findingByProductName[i].ProductName);

                }

                if(i >= findingByProductName.Count -1 && i >= findingSearchItemBrand.Count - 1 && i >= findingSearchItemByNestCategory.Count)
                {
                    break;
                }

                i++;
            }




            return Ok(combiningTheSearchData);
        }

        [HttpPost("SearchItems")]
        public async Task<IActionResult> SearchItems(SearchProductViewModel viewModel)
        {
            var foundProductList = new List<Business_Core.Entities.Product.Product>();
            var productsFoundList = new List<GetProductViewModel>();
            string toUpperCaseSearchStringFirstLetter = string.Concat(viewModel.SearchText[0]
                .ToString().ToUpper(), viewModel.SearchText.AsSpan(1));
            string upperCaseLettersSearchString = viewModel.SearchText.ToUpper();
            int countFoundProductByNestCategory = 0;
            int countFoundProductByBrand = 0;
            int countFoundProductByProductName = 0;



            // founding products by nest category 
            var findingSearchItemByNestCategory = await _dataContext
                .NestSubCategories.Where(a => a.NestSubCategoryName == viewModel.SearchText)
                .FirstOrDefaultAsync();

            if (findingSearchItemByNestCategory != null)
            {
                countFoundProductByNestCategory = await _dataContext.Products
                    .Where(a => a.NestSubCategoryId == findingSearchItemByNestCategory.NestSubCategoryID)
                    .CountAsync();
                    
                // pagination
                if (viewModel.PageNo == 1)
                {
                    foundProductList = await _dataContext
                              .Products.Include(a => a.ProductBrand)
                              .Include(a => a.ProductImages)
                              .Where(a => a.NestSubCategoryId == findingSearchItemByNestCategory.NestSubCategoryID)
                              .Take(12)
                              .ToListAsync();
                }
                else
                {
                    foundProductList = await _dataContext
                      .Products.Include(a => a.ProductBrand)
                      .Include(a => a.ProductImages)
                      .Where(a => a.NestSubCategoryId == findingSearchItemByNestCategory.NestSubCategoryID)
                      .Skip((viewModel.PageNo - 1) * 12)
                      .Take(12)
                      .ToListAsync();

                }

                foreach (var singleProduct in foundProductList)
                {
                    double ShowStarsByRatings = (double)singleProduct.ProductTotalStars / (singleProduct.Raitings * 5);
                    ShowStarsByRatings = ShowStarsByRatings * 5;
                    if (ShowStarsByRatings >= 0.3 && ShowStarsByRatings <= 0.7 ||  // 0.3 => 0.7 == 0.5
                  ShowStarsByRatings >= 1.3 && ShowStarsByRatings <= 1.7 ||
                  ShowStarsByRatings >= 2.3 && ShowStarsByRatings <= 2.7 ||
                  ShowStarsByRatings >= 3.3 && ShowStarsByRatings <= 3.7 ||
                  ShowStarsByRatings >= 4.3 && ShowStarsByRatings <= 4.7
                  )
                    {
                        ShowStarsByRatings = Math.Ceiling(ShowStarsByRatings) - 0.5;


                    }
                    else if (ShowStarsByRatings >= 0 && ShowStarsByRatings <= 0.2 || // 0 => 0.2 == 0
                      ShowStarsByRatings >= 1 && ShowStarsByRatings <= 1.2 ||
                      ShowStarsByRatings >= 2 && ShowStarsByRatings <= 2.2 ||
                       ShowStarsByRatings >= 3 && ShowStarsByRatings <= 3.2 ||
                       ShowStarsByRatings >= 4 && ShowStarsByRatings <= 4.2
                       )
                    {
                        ShowStarsByRatings = Math.Round(ShowStarsByRatings);


                    }
                    else if (ShowStarsByRatings > 0.7                           // 0.8 => 1 == 1
                       || ShowStarsByRatings > 1.7 || ShowStarsByRatings > 2.7
                       || ShowStarsByRatings > 3.7 || ShowStarsByRatings > 4.7)
                    {
                       ShowStarsByRatings = Math.Ceiling(ShowStarsByRatings);
                    }

                    productsFoundList.Add(new GetProductViewModel
                    {
                        ProductID = singleProduct.ProductID,
                        ProductBrandId = singleProduct.ProductBrandId,
                        ProductBrandName = singleProduct.ProductBrand.BrandName,
                        Color = singleProduct.Color,
                        GetProductImagess = null,
                        Price = singleProduct.Price,
                        ProductName = singleProduct.ProductName,
                        ImageUrl = singleProduct.ProductImages[0].URL,
                        ShowStarsByRatings = ShowStarsByRatings,
                        TotalProductStars = singleProduct.ProductTotalStars,
                        Raiting = singleProduct.Raitings

                    });


                }

                return Ok(new
                {
                    productsFoundData = productsFoundList,
                    productsFoundDataCount = countFoundProductByNestCategory
                });

            }

            // founding products by brands

            var findingSearchItemBrand = await _dataContext
                .ProductBrands.Where(a => a.BrandName == viewModel.SearchText)
                .FirstOrDefaultAsync();

            if (findingSearchItemBrand != null)
            {

                countFoundProductByBrand = await _dataContext.Products
                    .Where(a => a.ProductBrandId == findingSearchItemBrand.ProductBrandID)
                    .CountAsync();



                if (viewModel.PageNo == 1)
                {
                    foundProductList = await _dataContext.Products.Include(a => a.ProductBrand)
                                                  .Include(a => a.ProductImages)
                                                  .Where(a => a.ProductBrandId == findingSearchItemBrand.ProductBrandID)
                                                  .Take(12)
                                                  .ToListAsync();
                }
                else
                {
                    foundProductList = await _dataContext.Products
                        .Include(a => a.ProductBrand)
                                                 .Include(a => a.ProductImages)
                                                 .Where(a => a.ProductBrandId == findingSearchItemBrand.ProductBrandID)
                                                 .Skip((viewModel.PageNo - 1) * 12)
                                                 .Take(12)
                                                 .ToListAsync();
                }



                foreach (var singleProduct in foundProductList)
                {

                    double ShowStarsByRatings = (double)singleProduct.ProductTotalStars / (singleProduct.Raitings * 5);
                    ShowStarsByRatings = ShowStarsByRatings * 5;
                    if (ShowStarsByRatings >= 0.3 && ShowStarsByRatings <= 0.7 ||  // 0.3 => 0.7 == 0.5
                  ShowStarsByRatings >= 1.3 && ShowStarsByRatings <= 1.7 ||
                  ShowStarsByRatings >= 2.3 && ShowStarsByRatings <= 2.7 ||
                  ShowStarsByRatings >= 3.3 && ShowStarsByRatings <= 3.7 ||
                  ShowStarsByRatings >= 4.3 && ShowStarsByRatings <= 4.7
                  )
                    {
                        ShowStarsByRatings = Math.Ceiling(ShowStarsByRatings) - 0.5;


                    }
                    else if (ShowStarsByRatings >= 0 && ShowStarsByRatings <= 0.2 || // 0 => 0.2 == 0
                      ShowStarsByRatings >= 1 && ShowStarsByRatings <= 1.2 ||
                      ShowStarsByRatings >= 2 && ShowStarsByRatings <= 2.2 ||
                       ShowStarsByRatings >= 3 && ShowStarsByRatings <= 3.2 ||
                       ShowStarsByRatings >= 4 && ShowStarsByRatings <= 4.2
                       )
                    {
                        ShowStarsByRatings = Math.Round(ShowStarsByRatings);


                    }
                    else if (ShowStarsByRatings > 0.7                           // 0.8 => 1 == 1
                       || ShowStarsByRatings > 1.7 || ShowStarsByRatings > 2.7
                       || ShowStarsByRatings > 3.7 || ShowStarsByRatings > 4.7)
                    {
                        ShowStarsByRatings = Math.Ceiling(ShowStarsByRatings);
                    }



                    productsFoundList.Add(new GetProductViewModel
                    {
                        ProductID = singleProduct.ProductID,
                        ProductBrandId = singleProduct.ProductBrandId,
                        ProductBrandName = singleProduct.ProductBrand.BrandName,
                        Color = singleProduct.Color,
                        GetProductImagess = null,
                        Price = singleProduct.Price,
                        ProductName = singleProduct.ProductName,
                        ImageUrl = singleProduct.ProductImages[0].URL,
                        ShowStarsByRatings = ShowStarsByRatings,
                        TotalProductStars = singleProduct.ProductTotalStars,
                        Raiting = singleProduct.Raitings
                    });
                }
                return Ok(new
                {
                    productsFoundData = productsFoundList,
                    productsFoundDataCount = countFoundProductByBrand
                });
            }


                // search data by product name

            if(viewModel.PageNo == 1)
            {
                foundProductList = await _dataContext.Products
                .Include(a => a.ProductBrand)
                .Include(a => a.ProductImages)
                .Where(a => a.ProductName.Contains(viewModel.SearchText)) 
                .Take(12)
                .ToListAsync();

                //||
                // a.ProductName.Contains(upperCaseLettersSearchString) ||
                // a.ProductName.Contains(toUpperCaseSearchStringFirstLetter))
            }
            else
            {
                 foundProductList =  await _dataContext.Products
                .Include(a => a.ProductBrand)
                .Include(a => a.ProductImages)
                .Where(a => a.ProductName
                .Contains(viewModel.SearchText))
                .Skip((viewModel.PageNo - 1) * 12)
                .Take(12)
                .ToListAsync();
            }

            if(foundProductList.Count > 0)
            {
                //a.ProductName.Contains(upperCaseLettersSearchString) ||
                //a.ProductName.Contains(toUpperCaseSearchStringFirstLetter))

                countFoundProductByProductName = await _dataContext
                .Products.Where(a => a.ProductName.Contains(viewModel.SearchText))
                .CountAsync();  

                foreach (var singleProduct in foundProductList)
                 {

                    double ShowStarsByRatings = (double)singleProduct.ProductTotalStars / (singleProduct.Raitings * 5);
                    ShowStarsByRatings = ShowStarsByRatings * 5;
                    if (ShowStarsByRatings >= 0.3 && ShowStarsByRatings <= 0.7 ||  // 0.3 => 0.7 == 0.5
                  ShowStarsByRatings >= 1.3 && ShowStarsByRatings <= 1.7 ||
                  ShowStarsByRatings >= 2.3 && ShowStarsByRatings <= 2.7 ||
                  ShowStarsByRatings >= 3.3 && ShowStarsByRatings <= 3.7 ||
                  ShowStarsByRatings >= 4.3 && ShowStarsByRatings <= 4.7
                  )
                    {
                        ShowStarsByRatings = Math.Ceiling(ShowStarsByRatings) - 0.5;


                    }
                    else if (ShowStarsByRatings >= 0 && ShowStarsByRatings <= 0.2 || // 0 => 0.2 == 0
                      ShowStarsByRatings >= 1 && ShowStarsByRatings <= 1.2 ||
                      ShowStarsByRatings >= 2 && ShowStarsByRatings <= 2.2 ||
                       ShowStarsByRatings >= 3 && ShowStarsByRatings <= 3.2 ||
                       ShowStarsByRatings >= 4 && ShowStarsByRatings <= 4.2
                       )
                    {
                        ShowStarsByRatings = Math.Round(ShowStarsByRatings);


                    }
                    else if (ShowStarsByRatings > 0.7                           // 0.8 => 1 == 1
                       || ShowStarsByRatings > 1.7 || ShowStarsByRatings > 2.7
                       || ShowStarsByRatings > 3.7 || ShowStarsByRatings > 4.7)
                    {
                        ShowStarsByRatings = Math.Ceiling(ShowStarsByRatings);
                    }
                    productsFoundList.Add(new GetProductViewModel
                    {
                        ProductID = singleProduct.ProductID,
                        ProductBrandId = singleProduct.ProductBrandId,
                        ProductBrandName = singleProduct.ProductBrand.BrandName,
                        Color = singleProduct.Color,
                        GetProductImagess = null,
                        Price = singleProduct.Price,
                        ProductName = singleProduct.ProductName,
                        ImageUrl = singleProduct.ProductImages[0].URL,
                        ShowStarsByRatings = ShowStarsByRatings,
                        TotalProductStars = singleProduct.ProductTotalStars,
                        Raiting = singleProduct.Raitings
                    });
                }
                return Ok(new
                {
                    productsFoundData = productsFoundList,
                    productsFoundDataCount = countFoundProductByProductName
                });
            }

            return BadRequest("Sorry your result is not found " + viewModel.SearchText);
        }


    }

}
