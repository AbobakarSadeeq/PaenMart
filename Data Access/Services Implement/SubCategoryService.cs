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
    public class SubCategoryService : ISubCategoryService
    {
        private readonly IUnitofWork _unitofWork;

        public SubCategoryService(IUnitofWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }

        public async Task<SubCategory> DeleteSubCategory(SubCategory subCategory)
        {
            _unitofWork._SubCategoryRepository.DeleteAsync(subCategory);
            await _unitofWork.CommitAsync();
            return subCategory;
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategories(int singleCategoryId)
        {
            return await _unitofWork._SubCategoryRepository
                .GetAllSubCategoryByCategory(singleCategoryId);

        }

        public async Task<IEnumerable<SubCategory>> GetSubCategoriesForTable()
        {
            return await _unitofWork._SubCategoryRepository.GetAllSubCategories(); 

        }

        public async Task<SubCategory> GetSubCategory(int Id)
        {

            return await _unitofWork._SubCategoryRepository.GetByKeyAsync(Id);
        }

        public async Task<SubCategory> InsertSubCategory(SubCategory subCategory)
        {
            subCategory.Created_At = DateTime.Now;
            await _unitofWork._SubCategoryRepository.AddAsync(subCategory);
            await _unitofWork.CommitAsync();
            return subCategory;
        }

        public async Task<SubCategory> UpdateSubCategory(SubCategory OldData, SubCategory UpdateData)
        { 
            OldData.SubCategoryName = UpdateData.SubCategoryName; 
            OldData.CategoryId = UpdateData.CategoryId; 
            await _unitofWork.CommitAsync(); 
            return OldData;
        }
    }
}
