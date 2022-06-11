using Business_Core.Entities.Product;
using Business_Core.Entities.Product.Product_Images;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace Business_Core.IServices
{
    public interface IProductService
    {
        Task<Product> InsertProduct(Product product, List<IFormFile> File);
        Task<IEnumerable<Product>> GetProduct();
        Task<Product> GetProduct(int Id);
        void DeleteProductData(int Id);
        Task<Product> UpdateProduct(Product OldData, Product UpdateData);

        Task<GetProduct> GetSingleProduct(int Id);

        Task<IEnumerable<Product>> GetProducts(PageSelectedAndNestCategoryId pageSelectedAndNestCategoryId);
        Task<IEnumerable<GetProduct>> GetProductsByBrandId(PageSelectedAndNestCategoryId productByBrands);
        Task<IEnumerable<GetProduct>> GetProductsByNestSubCategoryId(PageSelectedAndNestCategoryId pageSelectedAndNestCategoryId);

        void DeletingSingleImageProduct(string imagesId);

        void UpdateProductImages(int productId, List<IFormFile> File);

        Task<IEnumerable<GetProduct>> GetFiveMostSelledProducts();

    }
}
