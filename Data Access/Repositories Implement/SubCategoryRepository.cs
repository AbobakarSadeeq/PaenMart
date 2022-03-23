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
    public class SubCategoryRepository : Repository<int, SubCategory> , ISubCategoryRepository 
    {
     
        private readonly DataContext _DataContext;
    public SubCategoryRepository(DataContext DataContext) : base(DataContext)
    {
        _DataContext = DataContext;
    }

        // Relationship
        public async Task<IEnumerable<SubCategory>> GetAllSubCategoryByCategory(int categoryId)
        {
            return await _DataContext.SubCategories.Include(a=>a.NestSubCategories)
                .Where(a => a.CategoryId == categoryId)
                .ToListAsync();

        }
    }


}
