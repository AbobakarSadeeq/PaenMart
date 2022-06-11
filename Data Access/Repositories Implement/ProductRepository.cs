using Business_Core.Entities.Product;
using Business_Core.Entities.Product.Product_Images;
using Business_Core.IRepositories;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Presentation.AppSettingClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories_Implement
{
    public class ProductRepository : Repository<int, Product>, IProductRepository
    {
        private readonly DataContext _DataContext;
        private readonly Cloudinary _cloudinary;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;



        public ProductRepository(DataContext DataContext, IOptions<CloudinarySettings> cloudinaryConfig) : base(DataContext)
        {
            _DataContext = DataContext;
            _cloudinaryConfig = cloudinaryConfig;


            Account acc = new Account(
                 _cloudinaryConfig.Value.CloudName,
                  _cloudinaryConfig.Value.ApiKey,
                  _cloudinaryConfig.Value.ApiSecret
                                                      );

            _cloudinary = new Cloudinary(acc);
        }

        // for Admin Side getAll product table
        public async Task<IEnumerable<Product>> GetAll(PageSelectedAndNestCategoryId pageSelectedAndNestCategoryId)
        {
            int findingSelectedCategoryProductNo = _DataContext.Products
                .Where(a => a.NestSubCategoryId == pageSelectedAndNestCategoryId.NestCategoryId)
                .Count();
            pageSelectedAndNestCategoryId.singleCategoryTotalProductsCount = findingSelectedCategoryProductNo;

            // 12 rows on first page and so on...
            if (pageSelectedAndNestCategoryId.PageSelectedNo == 1)
            {
                var gettingProducts = await _DataContext.Products.Include(a => a.ProductBrand)
               .Include(b => b.NestSubCategory)
               .Where(a => a.NestSubCategoryId == pageSelectedAndNestCategoryId.NestCategoryId)
               .Take(12).ToListAsync();
                return gettingProducts;


            }
            else
            {
                var gettingProducts = await _DataContext.Products.Include(a => a.ProductBrand)
                              .Include(b => b.NestSubCategory)
                              .Where(a => a.NestSubCategoryId == pageSelectedAndNestCategoryId.NestCategoryId)
                              .Skip((pageSelectedAndNestCategoryId.PageSelectedNo - 1) * 12).Take(12).ToListAsync();
                return gettingProducts;
            }

            
              
          


            
             




            //var gettingProducts = await _DataContext.Products.Include(a => a.ProductBrand)
            //    .Include(a => a.ProductImages).ToListAsync();
            //// .Include(b=>b.NestSubCategory) not required data
            //List<GetProduct> filterdProducts = new List<GetProduct>();
            //List<GetProductImages> getOnlyOneImage = new List<GetProductImages>();
            //foreach (var item in gettingProducts)
            //{

            //    if (item.ProductImages.Count > 0)
            //    {
            //        getOnlyOneImage.Add(new GetProductImages
            //        {
            //            ProductId = item.ProductImages[0].ProductId,
            //            ProductImageID = item.ProductImages[0].ProductImageID,
            //            PublicId = item.ProductImages[0].PublicId,
            //            URL = item.ProductImages[0].URL
            //        });
            //    }


            //    filterdProducts.Add(new GetProduct
            //    {
            //        ProductBrandName = item.ProductBrand.BrandName,
            //        //   NestSubCategoryName = item.NestSubCategory.NestSubCategoryName,
            //        ProductName = item.ProductName,
            //        Color = item.Color,
            //        Price = item.Price,
            //        StockAvailiability = item.StockAvailiability,
            //        Quantity = item.Quantity,
            //        SellUnits = item.SellUnits,
            //        ProductDetails = item.ProductDetails,
            //        ProductID = item.ProductID,
            //        Modified_at = item.Modified_at,
            //        Created_At = item.Created_At,
            //        GetProductImagess = getOnlyOneImage
            //    });
            //    getOnlyOneImage = new List<GetProductImages>();
            //}
            //return filterdProducts;
        }

 

        // for when specific Nest-Sub-Category is selected 
        public async Task<IEnumerable<GetProduct>> GetAllProductsByNestSubCategory(PageSelectedAndNestCategoryId pageSelectedAndNestCategoryId)
        {
            //var selectedNestCategoryProducts = await _DataContext.Products.Include(a => a.ProductBrand)
            //    .Include(a => a.ProductImages).Where(a => a.NestSubCategoryId == pageSelectedAndNestCategoryId.NestCategoryId).ToListAsync();

            var selectedNestCategoryProducts = new List<Product>();


            int foundedDataCount = await _DataContext
                .Products
                .Where(a => a.NestSubCategoryId == pageSelectedAndNestCategoryId.NestCategoryId)
                .CountAsync();
            pageSelectedAndNestCategoryId.singleCategoryTotalProductsCount = foundedDataCount;
            if (pageSelectedAndNestCategoryId.PageSelectedNo == 1)
            {
                selectedNestCategoryProducts =    await _DataContext.Products.Include(a => a.ProductBrand)
                .Include(a => a.ProductImages)
                .Where(a => a.NestSubCategoryId == pageSelectedAndNestCategoryId.NestCategoryId)
                .Take(24)
                .ToListAsync();

            }
            else
            {
                selectedNestCategoryProducts = await _DataContext.Products
               .Include(a => a.ProductBrand)
               .Include(a => a.ProductImages)
               .Where(a => a.NestSubCategoryId == pageSelectedAndNestCategoryId.NestCategoryId)
               .Skip((pageSelectedAndNestCategoryId.PageSelectedNo - 1) * 24)
               .Take(24)
               .ToListAsync();
         
            }


            List<GetProduct> filterdProducts = new List<GetProduct>();
            List<GetProductImages> getOnlyOneImage = new List<GetProductImages>();
            foreach (var item in selectedNestCategoryProducts)
            {

                if (item.ProductImages.Count > 0)
                {
                    getOnlyOneImage.Add(new GetProductImages
                    {
                        ProductId = item.ProductImages[0].ProductId,
                        ProductImageID = item.ProductImages[0].ProductImageID,
                        PublicId = item.ProductImages[0].PublicId,
                        URL = item.ProductImages[0].URL
                    });
                }


                filterdProducts.Add(new GetProduct
                {
                    ProductBrandName = item.ProductBrand.BrandName,
                    ProductBrandId = item.ProductBrand.ProductBrandID,
                    ProductName = item.ProductName,
                    Color = item.Color,
                    Price = item.Price,
                    StockAvailiability = item.StockAvailiability,
                    Quantity = item.Quantity,
                    SellUnits = item.SellUnits,
                    ProductDetails = item.ProductDetails,
                    ProductID = item.ProductID,
                    Modified_at = item.Modified_at,
                    Created_At = item.Created_At,
                    GetProductImagess = getOnlyOneImage
                });
                getOnlyOneImage = new List<GetProductImages>();
            }
            return filterdProducts;
        }

        // for when specific brand is selected 

        public async Task<IEnumerable<GetProduct>> GetAllProductsByBrand(PageSelectedAndNestCategoryId productByBrands)
        {
            var selectedBrandProducts = new List<Product>();
            int foundedDataCount = await _DataContext
                .Products
                .Where(a => a.NestSubCategoryId == productByBrands.NestCategoryId && a.ProductBrandId == productByBrands.BrandId)
                .CountAsync();
            productByBrands.singleCategoryTotalProductsCount = foundedDataCount;

            if (productByBrands.PageSelectedNo == 1)
            {
                selectedBrandProducts = await _DataContext.Products
                .Include(a => a.ProductBrand)
                .Include(a => a.ProductImages)
                .Where(a => a.ProductBrandId == productByBrands.BrandId && a.NestSubCategoryId == productByBrands.NestCategoryId)
                .Take(24)
                .ToListAsync();

            }
            else
            {
                selectedBrandProducts = await _DataContext.Products
               .Include(a => a.ProductBrand)
               .Include(a => a.ProductImages)
               .Where(a => a.ProductBrandId == productByBrands.BrandId && a.NestSubCategoryId == productByBrands.NestCategoryId)
               .Skip((productByBrands.PageSelectedNo - 1) * 24)
               .Take(24)
               .ToListAsync();
            }

            List<GetProduct> filterdProducts = new List<GetProduct>();
            List<GetProductImages> getOnlyOneImage = new List<GetProductImages>();
            foreach (var item in selectedBrandProducts)
            {

                if (item.ProductImages.Count > 0)
                {
                    getOnlyOneImage.Add(new GetProductImages
                    {
                        ProductId = item.ProductImages[0].ProductId,
                        ProductImageID = item.ProductImages[0].ProductImageID,
                        PublicId = item.ProductImages[0].PublicId,
                        URL = item.ProductImages[0].URL
                    });
                }


                filterdProducts.Add(new GetProduct
                {
                    ProductBrandName = item.ProductBrand.BrandName,
                    ProductBrandId = item.ProductBrand.ProductBrandID,
                    ProductName = item.ProductName,
                    Color = item.Color,
                    Price = item.Price,
                    StockAvailiability = item.StockAvailiability,
                    Quantity = item.Quantity,
                    SellUnits = item.SellUnits,
                    ProductDetails = item.ProductDetails,
                    ProductID = item.ProductID,
                    Modified_at = item.Modified_at,
                    Created_At = item.Created_At,
                    GetProductImagess = getOnlyOneImage
                });
                getOnlyOneImage = new List<GetProductImages>();
            }
            return filterdProducts;
        }


        public async Task<GetProduct> GetProductById(int Id)
        {
            var gettingDataById = await _DataContext.Products.Include(a => a.ProductBrand)
                .Include(a=>a.NestSubCategory).Include(a=>a.ProductImages)
                .FirstOrDefaultAsync(a => a.ProductID == Id);

 



            List<GetProductImages> filterdImages = new List<GetProductImages>();

            foreach (var singleImage in gettingDataById.ProductImages)
            {
                filterdImages.Add(new GetProductImages { ProductId = singleImage.ProductId,
                    ProductImageID = singleImage.ProductImageID,
                    PublicId = singleImage.PublicId,
                    URL = singleImage.URL });
            }

            return new GetProduct
            {
                ProductBrandId = gettingDataById.ProductBrand.ProductBrandID,
                ProductBrandName = gettingDataById.ProductBrand.BrandName,
                NestSubCategoryId = gettingDataById.NestSubCategory.NestSubCategoryID,
                NestSubCategoryName = gettingDataById.NestSubCategory.NestSubCategoryName,
                ProductName = gettingDataById.ProductName,
                Color = gettingDataById.Color,
                Price = gettingDataById.Price,
                StockAvailiability = gettingDataById.StockAvailiability,
                Quantity = gettingDataById.Quantity,
                SellUnits = gettingDataById.SellUnits,
                ProductDetails = gettingDataById.ProductDetails,
                ProductID = gettingDataById.ProductID,
                Modified_at = gettingDataById.Modified_at,
                Created_At = gettingDataById.Created_At,
                GetProductImagess = filterdImages
            };

        }

        // Adding Product images
        public IList<ProductImages> AddProductImage(List<IFormFile> File)
        {
            var addingProductPhotos = new List<ProductImages>();
            foreach (var file in File)
            {
                var uploadResult = new ImageUploadResult();
                if (File.Count > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadparams = new ImageUploadParams
                        {
                            File = new FileDescription(file.Name, stream),
                            // we can also crop the image if we want here means when user could upload his large size or big shape of image then crop it all its around thing just focus it on the face only
                            // it will crop the image automatically for us. 
                            Transformation = new Transformation()
                            .Width(824).Height(536)

                        };
                        // Uploading the image on clodinary server and could take a while
                        uploadResult = _cloudinary.Upload(uploadparams);
                    }
                }
                addingProductPhotos.
                Add(new ProductImages
                {
                    PublicId = uploadResult.PublicId,
                    URL = uploadResult.Url.ToString()
                });
            }

            return addingProductPhotos;
 

        }

        public void DeleteProduct(int productId)
        {
            var product = _DataContext.Products.Include(a => a.ProductImages)
                .FirstOrDefault(a=>a.ProductID == productId);
            foreach (var item in product.ProductImages)
            {
                var deletePrams = new DeletionParams(item.PublicId);
                var cloudinaryDeletePhoto = _cloudinary.Destroy(deletePrams);
            }
            _DataContext.Products.Remove(product);
        }

        public void DeleteSingleImageOfProduct(string singleImageId)
        {
            var findingImageId =  _DataContext.ProductImages
                .FirstOrDefault(a => a.PublicId == singleImageId);
            _DataContext.ProductImages.Remove(findingImageId);
            _DataContext.SaveChanges();
            var deletePrams = new DeletionParams(singleImageId);
            var cloudinaryDeletePhoto = _cloudinary.Destroy(deletePrams);
        }


        // adding or updating single product images
        public async void UpdateProductImage(int productId, List<IFormFile> File)
        {
            var addingProductPhotos = new List<ProductImages>();
            foreach (var file in File)
            {
                var uploadResult = new ImageUploadResult();
                if (File.Count > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadparams = new ImageUploadParams
                        {
                            File = new FileDescription(file.Name, stream),
                            // we can also crop the image if we want here means when user could upload his large size or big shape of image then crop it all its around thing just focus it on the face only
                            // it will crop the image automatically for us. 
                            Transformation = new Transformation()
                            .Width(824).Height(536)

                        };
                        // Uploading the image on clodinary server and could take a while
                        uploadResult = _cloudinary.Upload(uploadparams);
                    }
                }
                addingProductPhotos.
                 Add(new ProductImages
                 {
                     PublicId = uploadResult.PublicId,
                     URL = uploadResult.Url.ToString(),
                     ProductId = productId
                 });
            }
            _DataContext.ProductImages.AddRange(addingProductPhotos);
              _DataContext.SaveChanges();

        }

        public async Task<IEnumerable<GetProduct>> GetMostSellFiveProducts()
        
        {

            var gettingData = await _DataContext.Products
                .Include(a => a.ProductBrand)
                .Include(a => a.NestSubCategory)
                .Include(a => a.ProductImages)
                .Take(5)
                .ToListAsync();
            // return await  _DataContext.Products.OrderByDescending(a => a.SellUnits).Take(5).ToListAsync();



            List<GetProduct> filterdProducts = new List<GetProduct>();
            List<GetProductImages> getOnlyOneImage = new List<GetProductImages>();
            foreach (var item in gettingData)
            {

                if (item.ProductImages.Count > 0)
                {
                    getOnlyOneImage.Add(new GetProductImages
                    {
                        ProductId = item.ProductImages[0].ProductId,
                        ProductImageID = item.ProductImages[0].ProductImageID,
                        PublicId = item.ProductImages[0].PublicId,
                        URL = item.ProductImages[0].URL
                    });
                }


                filterdProducts.Add(new GetProduct
                {
                    ProductBrandName = item.ProductBrand.BrandName,
                    ProductName = item.ProductName,
                    Color = item.Color,
                    Price = item.Price,
                    ProductID = item.ProductID,
                    GetProductImagess = getOnlyOneImage
                });
                getOnlyOneImage = new List<GetProductImages>();
            }
            return filterdProducts;
        }
    }
}
