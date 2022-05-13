using Business_Core.Entities;
using Business_Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.IRepositories
{
    public interface IProductBrandRepository : IRepository<int, ProductBrand>
    {
        Task<IEnumerable<Object>> GetAllBrandByNestSubCategory(int categoryId);
        Task<IEnumerable<ProductBrand>> GetListOfBrands();

        // NestSubCategoryProductBrand Crud

        Task AddDataToNestSubProductBrand(NestSubCategoryProductBrand nestSubCategoryProductBrand);

        Task<IList<ConvertFilterNestCategoryAndBrandData>> GetAllNestSubAndProductBrands();

        Task DeleteSingleNestSubCategoryProductBrand(int nestSubId, int brandId);

    }
}
