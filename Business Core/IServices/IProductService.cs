using Business_Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.IServices
{
    public interface IProductService
    {
        Task<Product> InsertProduct(Product product);
        Task<IEnumerable<Product>> GetProduct();
        Task<Product> GetProduct(int Id);
        Task<Product> DeleteProduct(Product product);
        Task<Product> UpdateProduct(Product OldData, Product UpdateData);

        Task<GetProduct> GetSingleProduct(int Id);

        Task<IEnumerable<GetProduct>> GetProducts();
        Task<IEnumerable<GetProduct>> GetProductsByBrandId(int brandId);
        Task<IEnumerable<GetProduct>> GetProductsByNestSubCategoryId(int NestCategoryId);


    }
}
