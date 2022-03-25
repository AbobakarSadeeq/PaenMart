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
    public class DynamicFormStructureRepository : Repository<int, DynamicFormStructure>, IDynamicFormStructureRepository
    {
        private readonly DataContext _DataContext;
        public DynamicFormStructureRepository(DataContext DataContext) : base(DataContext)
        {
            _DataContext = DataContext;
        }

        public async Task<IEnumerable<GetDynamicFormStructure>> GetAllFormStructure()
        {
            return from form in _DataContext.DynamicFormStructures
                   join nsc in _DataContext.NestSubCategories
                   on form.NestSubCategoryId equals nsc.NestSubCategoryID
                   select new GetDynamicFormStructure
                   {
                     DynamicFormStructureID = form.DynamicFormStructureID,
                     FormStructure =  form.FormStructure,
                     NestSubCategoryId = form.NestSubCategoryId,
                     NestSubCategoryName = nsc.NestSubCategoryName,
                     Created_At =  form.Created_At,
                   };
        }
    }
}
