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
        Task<IEnumerable<Object>> GetProductBrands(int singleNestSubCategoryId);
        Task<IEnumerable<ProductBrand>> GetAllBrands();
        Task<ProductBrand> GetProductBrand(int Id);
        Task<ProductBrand> DeleteProductBrand(ProductBrand productBrand);
        Task<ProductBrand> UpdateProductBrand(ProductBrand OldData, ProductBrand UpdateData);

        // NestSubCategoryProductBrand Crud
        Task AddNestSubCategoryProductBrand(NestSubCategoryProductBrand data);

        Task<IList<ConvertFilterNestCategoryAndBrandData>> GetAllNestSubAndProductBrands();

        Task DeleteDataFromNestAndBrand(int nestSubCategoryId, int productBrandId);


    }
}
