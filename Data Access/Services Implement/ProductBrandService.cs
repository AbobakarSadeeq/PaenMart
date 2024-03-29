﻿using Business_Core.Entities;
using Business_Core.Entities.Product;
using Business_Core.IServices;
using Business_Core.IUnitOfWork;
using Data_Access.DataContext_Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Services_Implement
{
    public class ProductBrandService : IProductBrandService
    {
        private readonly IUnitofWork _unitofWork;

        public ProductBrandService(IUnitofWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }

    

        public async Task<ProductBrand> DeleteProductBrand(ProductBrand productBrand)
        {
            _unitofWork._ProductBrandRepository.DeleteAsync(productBrand);
            await _unitofWork.CommitAsync();
            return productBrand;
        }

        public async Task<ProductBrand> GetProductBrand(int Id)
        {
            return await _unitofWork._ProductBrandRepository.GetByKeyAsync(Id);
        }

        public async Task<IEnumerable<Object>> GetProductBrands(int singleNestSubCategoryId)
        {
            return await _unitofWork._ProductBrandRepository.GetAllBrandByNestSubCategory(singleNestSubCategoryId);
        }

        public async Task<ProductBrand> InsertProductBrand(ProductBrand productBrand)
        {
            productBrand.Created_At = DateTime.Now;
            await _unitofWork._ProductBrandRepository.AddAsync(productBrand);
            await _unitofWork.CommitAsync();
            return productBrand;
        }

        public async Task<ProductBrand> UpdateProductBrand(ProductBrand OldData, ProductBrand UpdateData)
        {
            OldData.BrandName = UpdateData.BrandName;
            await _unitofWork.CommitAsync();
            return OldData;
        }

        // NestSubCategoryProductBrand Crud

        public async Task AddNestSubCategoryProductBrand(NestSubCategoryProductBrand data)
        {
            data.Created_At = DateTime.Now;
           await  _unitofWork._ProductBrandRepository.AddDataToNestSubProductBrand(data);
           await _unitofWork.CommitAsync();
        }

        public async Task<IList<ConvertFilterNestCategoryAndBrandData>> GetAllNestSubAndProductBrands()
        {
            return await  _unitofWork._ProductBrandRepository.GetAllNestSubAndProductBrands();
        }

        public async Task DeleteDataFromNestAndBrand(int nestSubCategoryId, int productBrandId)
        {
            await _unitofWork._ProductBrandRepository.DeleteSingleNestSubCategoryProductBrand(nestSubCategoryId, productBrandId);
            await _unitofWork.CommitAsync();
        }

        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await _unitofWork._ProductBrandRepository.GetListOfBrands();
        }
    }
}
