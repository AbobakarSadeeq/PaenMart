using AutoMapper;
using Business_Core.Entities;
using Business_Core.Entities.Product;
using Presentation.ViewModel;
using Presentation.ViewModel.ProductViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.AutoMapper
{
    public class AutoMappers : Profile
    {
        public AutoMappers()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();

            CreateMap<SubCategory, SubCategoryViewModel>().ReverseMap();
            CreateMap<SubCategory, GetSubCategoriesViewModel>()
               .ForMember(a => a.CategoryName,
 opts => opts.MapFrom(src => src.Category.CategoryName));

            CreateMap<NestSubCategory, NestSubCategoryViewModel>().ReverseMap();
            CreateMap<NestSubCategoryProductBrand, NestSubCategoryProductBrandViewModel>().ReverseMap();
            CreateMap<NestSubCategory, GetNestSubCategoryViewModel>()
               .ForMember(a => a.SubCategoryName,
 opts => opts.MapFrom(src => src.SubCategory.SubCategoryName));


            CreateMap<ProductBrand, ProductBrandViewModel>().ReverseMap();
            CreateMap<DynamicFormStructure, DynamicFormStructureViewModel>().ReverseMap();


            CreateMap<GetDynamicFormStructure, GetDynamicFormStructureViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<GetProduct, GetProductViewModel>().ReverseMap();

         

        }
    }
}
