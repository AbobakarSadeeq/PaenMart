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
    public class NestSubCategoryService : INestSubCategoryService
    {
        private readonly IUnitofWork _unitofWork;

        public NestSubCategoryService(IUnitofWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }
        public async Task<NestSubCategory> DeleteNestSubCategory(NestSubCategory nestSubCategory)
        {
            _unitofWork._NestSubCategoryRepository.DeleteAsync(nestSubCategory);
            await _unitofWork.CommitAsync();
            return nestSubCategory;
        }

        public async Task<IEnumerable<NestSubCategory>> GetNestSubCategories()
        {
            return await _unitofWork._NestSubCategoryRepository.GetNestSubCategory();
        }

        public async Task<IEnumerable<NestSubCategory>> GetNestSubCategoriesBySubCategoryId(int singleSubCategoryId)
        {
            return await _unitofWork._NestSubCategoryRepository
                 .GetAllNestSubCategoryBySubCategory(singleSubCategoryId);
        }

         

        public async Task<NestSubCategory> GetNestSubCategory(int Id)
        {
            return await _unitofWork._NestSubCategoryRepository.GetByKeyAsync(Id);
        }

        public async Task<NestSubCategory> InsertNestSubCategory(NestSubCategory nestSubCategory)
        {
            nestSubCategory.Created_At = DateTime.Now;
            await _unitofWork._NestSubCategoryRepository.AddAsync(nestSubCategory);
            await _unitofWork.CommitAsync();
            return nestSubCategory;
        }

        public async Task<NestSubCategory> UpdateNestSubCategory(NestSubCategory OldData, NestSubCategory UpdateData)
        {
            OldData.NestSubCategoryName = UpdateData.NestSubCategoryName;
            OldData.SubCategoryId = UpdateData.SubCategoryId;
            await _unitofWork.CommitAsync();
            return OldData;
        }
    }
}
