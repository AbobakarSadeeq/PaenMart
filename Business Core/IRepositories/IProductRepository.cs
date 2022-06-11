using Business_Core.Entities.Product;
using Business_Core.Entities.Product.Product_Images;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.IRepositories
{
    public interface IProductRepository : IRepository<int, Product>
    {
        // Get product data for admin table
        Task<IEnumerable<Product>> GetAll(PageSelectedAndNestCategoryId pageSelectedAndNestCategoryId);

        Task<GetProduct> GetProductById(int Id);
        Task<IEnumerable<GetProduct>> GetAllProductsByBrand(PageSelectedAndNestCategoryId productByBrands);
        Task<IEnumerable<GetProduct>> GetAllProductsByNestSubCategory(PageSelectedAndNestCategoryId pageSelectedAndNestCategoryId);

        IList<ProductImages> AddProductImage(List<IFormFile> File);

        // Delete Product 
        void DeleteProduct(int productId);
        void DeleteSingleImageOfProduct(string getSingleImageId);

        // updating product
        void UpdateProductImage(int productId, List<IFormFile> File);

        // get only five products that most sell-out

        Task<IEnumerable<GetProduct>> GetMostSellFiveProducts();

        // get brands
      // Task<IEnumerable<ProductBrand>> Get

    }
}
