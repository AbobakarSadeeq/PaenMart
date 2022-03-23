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

        public async Task<IEnumerable<ProductBrand>> GetAllBrandByNestSubCategory(int nestSubCategoryId)
        {
            return await _DataContext.ProductBrands
                .Where(a => a.NestSubCategoryId == nestSubCategoryId).ToListAsync();
        }
    }
}
