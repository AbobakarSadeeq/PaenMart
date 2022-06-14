using Business_Core.Entities;
using Business_Core.IRepositories;
using Data_Access.DataContext_Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories_Implement
{
    public class CategoryRepository : Repository<int, Category>, ICategoryRepository 
    {
        private readonly DataContext _DataContext;
        public CategoryRepository(DataContext DataContext) : base(DataContext)
        {
            _DataContext = DataContext;
        }
    }
}
