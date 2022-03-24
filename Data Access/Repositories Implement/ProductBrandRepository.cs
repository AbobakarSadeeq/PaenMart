using Business_Core.Entities;
using Business_Core.IRepositories;
using Data_Access.DataContext_Class;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories_Implement
{
    public class ProductBrandRepository : Repository<int, ProductBrand>, IProductBrandRepository
    {
        private readonly DataContext _DataContext;
        public ProductBrandRepository(DataContext DataContext) : base(DataContext)
        {
            _DataContext = DataContext;
        }

        

        public async Task<IEnumerable<Object>> GetAllBrandByNestSubCategory(int nestSubCategoryId)
        {
            var findingNestSubCategoryId = await _DataContext.NestSubCategoryProductBrands
                .Where(a => a.NestSubCategoryId == nestSubCategoryId).ToListAsync();

            var gettingDataByNesSubCategoryIdOfBrand = from a in findingNestSubCategoryId
                                                       join b in _DataContext.ProductBrands on a.ProductBrandId equals b.ProductBrandID
                                                       select new
                                                       {
                                                           BrandID = b.ProductBrandID,
                                                           BrandName = b.BrandName,
                                                           Created_At = b.Created_At
                                                       };
            return gettingDataByNesSubCategoryIdOfBrand;
        }

        // NestSubCategoryProductBrand Crud

        public async Task AddDataToNestSubProductBrand(NestSubCategoryProductBrand nestSubCategoryProductBrand)
        {
            await _DataContext.NestSubCategoryProductBrands.AddAsync(nestSubCategoryProductBrand);
        }
    }
}
