using Business_Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.IRepositories
{
    public interface IProductRepository : IRepository<int, Product>
    {
        Task<GetProduct> GetProductById(int Id);
        Task<IEnumerable<GetProduct>> GetAll();
        Task<IEnumerable<GetProduct>> GetAllProductsByBrand(int BrandId);
        Task<IEnumerable<GetProduct>> GetAllProductsByNestSubCategory(int NestSubCategoryId);

    }
}
