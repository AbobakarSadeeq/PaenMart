using Business_Core.Entities;
using Business_Core.IServices;
using Business_Core.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Services_Implement
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitofWork _unitofWork;
        public CategoryService(IUnitofWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }

        public async Task<Category> DeleteCategory(Category category)
        {
            _unitofWork._CategoryRepository.DeleteAsync(category);
            await _unitofWork.CommitAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _unitofWork._CategoryRepository.GetAllSync();
        }

        public async Task<Category> GetCategory(int Id)
        {
            return await _unitofWork._CategoryRepository.GetByKeyAsync(Id);
        }

        public async Task<Category> InsertCategory(Category category)
        {
            category.Created_At = DateTime.Now;
            await _unitofWork._CategoryRepository.AddAsync(category);
            await _unitofWork.CommitAsync();
            return category;
        }

        public async Task<Category> UpdateCategory(Category OldData, Category UpdateData)
        {
            // if you having a modify property of an entity then assign DateTime value here
            OldData.CategoryName = UpdateData.CategoryName;
            await _unitofWork.CommitAsync();
            return OldData;
        }
    }
}
