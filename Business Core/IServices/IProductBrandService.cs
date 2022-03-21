using Business_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.IServices
{
    public interface IProductBrandService
    {
        Task<ProductBrand> InsertProductBrand(ProductBrand  productBrand);
        Task<IEnumerable<ProductBrand>> GetProductBrands();
        Task<ProductBrand> GetProductBrand(int Id);
        Task<ProductBrand> DeleteProductBrand(ProductBrand productBrand);
        Task<ProductBrand> UpdateProductBrand(ProductBrand OldData, ProductBrand UpdateData);
    }
}
