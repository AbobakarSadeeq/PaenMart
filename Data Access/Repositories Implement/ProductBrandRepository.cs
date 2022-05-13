using Business_Core.Entities;
using Business_Core.Entities.Product;
using Business_Core.IRepositories;
using Data_Access.DataContext_Class;
using Microsoft.EntityFrameworkCore;

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

            // inner joining applied
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


        public async Task<IEnumerable<ProductBrand>> GetListOfBrands()
        {
            return await _DataContext.ProductBrands.ToListAsync();
        }

        // NestSubCategoryProductBrand Crud

        public async Task AddDataToNestSubProductBrand(NestSubCategoryProductBrand nestSubCategoryProductBrand)
        {
            await _DataContext.NestSubCategoryProductBrands.AddAsync(nestSubCategoryProductBrand);
        }

        public async Task<IList<ConvertFilterNestCategoryAndBrandData>> GetAllNestSubAndProductBrands()
        {

            var joiningMultipleTables = from np in _DataContext.NestSubCategoryProductBrands
                                        join pb in _DataContext.ProductBrands on np.ProductBrandId equals pb.ProductBrandID
                                        join nsc in _DataContext.NestSubCategories on np.NestSubCategoryId equals nsc.NestSubCategoryID
                                        select new NestSubCategoryProductBrandJoining
                                        {
                                            NestSubCategoryName = nsc.NestSubCategoryName,
                                            BrandName = pb.BrandName,
                                            NestSubCategoryId = np.NestSubCategoryId,
                                            ProductBrandId = np.ProductBrandId,
                                            NestSubCategoryProductBrandCreated_At = np.Created_At
                                        };

            var myDict = new Dictionary<string, List<FilterNestSubCategoryWithBrand>>();
            var list = await joiningMultipleTables.ToListAsync();
            var temp = new List<FilterNestSubCategoryWithBrand>();

            foreach (var item in list)
            {
                if (myDict.ContainsKey(item.BrandName))
                {
                    var oldValue = myDict[item.BrandName];
                    oldValue.Add(new FilterNestSubCategoryWithBrand { BrandName = item.BrandName, BrandId = item.ProductBrandId, NestCategoryId = item.NestSubCategoryId, NestCategoryName = item.NestSubCategoryName });
                    myDict[item.BrandName] = oldValue; // old value replaced with new now

                }
                else
                {
                    temp.Add(new FilterNestSubCategoryWithBrand { BrandName = item.BrandName, BrandId = item.ProductBrandId, NestCategoryId = item.NestSubCategoryId, NestCategoryName = item.NestSubCategoryName });
                    myDict.Add(item.BrandName, temp);

                }
                temp = new List<FilterNestSubCategoryWithBrand>();

            }

            var convertingToList = new List<ConvertFilterNestCategoryAndBrandData>();

            foreach (var item in myDict)
            {
                convertingToList.Add(new ConvertFilterNestCategoryAndBrandData { BrandName = item.Key, NestSubCategoryWithBrands = item.Value });
            }



            return convertingToList;

        }

        public async Task DeleteSingleNestSubCategoryProductBrand(int nestSubId, int brandId)
        {
            var findingDataForDeletion = await _DataContext.NestSubCategoryProductBrands
                .FirstOrDefaultAsync(a => a.NestSubCategoryId == nestSubId && a.ProductBrandId == brandId);
            _DataContext.NestSubCategoryProductBrands.Remove(findingDataForDeletion);
        }




    }


}
