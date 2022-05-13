using Business_Core.IRepositories;
using Business_Core.IUnitOfWork;
using Data_Access.DataContext_Class;
using Data_Access.Repositories_Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.UnitOfWork
{
    public class UnitofWork : IUnitofWork
    {
        private readonly DataContext _DataContext;

        public ICategoryRepository _CategoryRepository { get; init; }

        public ISubCategoryRepository _SubCategoryRepository { get; init; }

        public INestSubCategoryRepository _NestSubCategoryRepository { get; init; }

        public IProductBrandRepository _ProductBrandRepository { get; init; }
        public IProductRepository _ProductRepository { get; init; }


        public IDynamicFormStructureRepository _DynamicFormStructureRepository { get; init; }
        public UnitofWork(DataContext DataContext)
        {
            _DataContext = DataContext;

            _CategoryRepository = new CategoryRepository(_DataContext);
            _SubCategoryRepository = new SubCategoryRepository(_DataContext);
            _NestSubCategoryRepository = new NestSubCategoryRepository(_DataContext);
            _ProductBrandRepository = new ProductBrandRepository(_DataContext);
            _DynamicFormStructureRepository = new DynamicFormStructureRepository(_DataContext);
            _ProductRepository = new ProductRepository(_DataContext);


        }

        public async  Task<int> CommitAsync()
        {
            return await _DataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _DataContext.Dispose();
        }
    }
}
