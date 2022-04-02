using Business_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.IServices
{
    public interface ISubCategoryService
    {
        Task<SubCategory> InsertSubCategory(SubCategory subCategory);
        Task<IEnumerable<SubCategory>> GetSubCategories(int singleCategoryId);
        Task<IEnumerable<SubCategory>> GetSubCategoriesForTable();

        Task<SubCategory> GetSubCategory(int Id);

        Task<SubCategory> DeleteSubCategory(SubCategory subCategory);
        Task<SubCategory> UpdateSubCategory(SubCategory OldData, SubCategory UpdateData);
    }
}
