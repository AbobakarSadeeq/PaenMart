using Business_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.IServices
{
    public interface INestSubCategoryService
    {
        Task<NestSubCategory> InsertNestSubCategory(NestSubCategory nestSubCategory);
        Task<IEnumerable<NestSubCategory>> GetNestSubCategoriesBySubCategoryId(int singleSubCategoryId);
        Task<IEnumerable<NestSubCategory>> GetNestSubCategories();

        Task<NestSubCategory> GetNestSubCategory(int Id);
        Task<NestSubCategory> DeleteNestSubCategory(NestSubCategory nestSubCategory);
        Task<NestSubCategory> UpdateNestSubCategory(NestSubCategory OldData, NestSubCategory UpdateData);
    }
}
