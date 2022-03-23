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
    public class NestSubCategoryRepository : Repository<int, NestSubCategory>, INestSubCategoryRepository
    {
        private readonly DataContext _DataContext;
        public NestSubCategoryRepository(DataContext DataContext) : base(DataContext)
        {
            _DataContext = DataContext;
        }

        public async Task<IEnumerable<NestSubCategory>> GetAllNestSubCategoryBySubCategory(int subCategoryId)
        {
            return await _DataContext.NestSubCategories
                .Where(a => a.SubCategoryId == subCategoryId)
                .ToListAsync();
        }
    }
}
