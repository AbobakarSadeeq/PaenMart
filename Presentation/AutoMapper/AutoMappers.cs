using AutoMapper;
using Business_Core.Entities;
using Presentation.ViewModel;
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
            CreateMap<NestSubCategory, NestSubCategoryViewModel>().ReverseMap();
            CreateMap<ProductBrand, ProductBrandViewModel>().ReverseMap();
            CreateMap<DynamicFormStructure, DynamicFormStructureViewModel>().ReverseMap();
            CreateMap<NestSubCategoryProductBrand, NestSubCategoryProductBrandViewModel>().ReverseMap();
            CreateMap<GetDynamicFormStructure, GetDynamicFormStructureViewModel>().ReverseMap();

        }
    }
}
