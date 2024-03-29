﻿using Business_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.IRepositories
{
    public interface INestSubCategoryRepository : IRepository<int, NestSubCategory>
    {
        Task<IEnumerable<NestSubCategory>> GetAllNestSubCategoryBySubCategory(int subCategoryId);
        Task<IEnumerable<NestSubCategory>> GetNestSubCategory();

    }
}
