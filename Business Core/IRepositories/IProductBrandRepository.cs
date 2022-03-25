using Business_Core.Entities;
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

        // NestSubCategoryProductBrand Crud

        Task AddDataToNestSubProductBrand(NestSubCategoryProductBrand nestSubCategoryProductBrand);

        IQueryable<NestSubCategoryProductBrandJoining> GetAllNestSubAndProductBrands();

        Task DeleteSingleNestSubCategoryProductBrand(int nestSubId, int brandId);

    }
}
