using Business_Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.IUnitOfWork
{
    public interface IUnitofWork
    {
        ICategoryRepository _CategoryRepository { get; }
        ISubCategoryRepository _SubCategoryRepository { get; }
        INestSubCategoryRepository _NestSubCategoryRepository { get; }
        IProductBrandRepository _ProductBrandRepository { get; }
        IDynamicFormStructureRepository _DynamicFormStructureRepository { get; }

        Task<int> CommitAsync();
        void Dispose();
    }
}
