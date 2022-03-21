using Business_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.IServices
{
    public interface ICategoryService
    {
        Task<Category> InsertCategory(Category category);
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int Id);
        Task<Category> DeleteCategory(Category category);
        Task<Category> UpdateCategory(Category OldData, Category UpdateData);
    }
}
