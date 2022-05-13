using Business_Core.Entities.Product;
using Business_Core.Entities.Product.Product_Images;
using Business_Core.IRepositories;
using Data_Access.DataContext_Class;
using Microsoft.EntityFrameworkCore;
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
        public ProductRepository(DataContext DataContext) : base(DataContext)
        {
            _DataContext = DataContext;
        }

        // for admin specific table only 
        public async Task<IEnumerable<GetProduct>> GetAll()
        {
            var gettingProducts = await _DataContext.Products.Include(a => a.ProductBrand)
                .Include(a => a.ProductImages).ToListAsync();
            // .Include(b=>b.NestSubCategory) not required data
            List<GetProduct> filterdProducts = new List<GetProduct>();
            List<GetProductImages> getOnlyOneImage = new List<GetProductImages>();
            foreach (var item in gettingProducts)
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
                    //   NestSubCategoryName = item.NestSubCategory.NestSubCategoryName,
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

        // for when specific Nest-Sub-Category is selected 
        public async Task<IEnumerable<GetProduct>> GetAllProductsByNestSubCategory(int NestSubCategoryId)
        {
            var selectedNestCategoryProducts = await _DataContext.Products.Include(a => a.ProductBrand)
                .Include(a => a.ProductImages).Where(a => a.NestSubCategoryId == NestSubCategoryId).ToListAsync();

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

        public async Task<IEnumerable<GetProduct>> GetAllProductsByBrand(int BrandId)
        {
            var selectedBrandProducts = await _DataContext.Products.Include(a => a.ProductBrand)
                .Include(a => a.ProductImages).Where(a => a.ProductBrandId == BrandId).ToListAsync();

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
    }
}
