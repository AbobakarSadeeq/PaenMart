using AutoMapper;
using Business_Core.Entities;
using Business_Core.Entities.Carousel;
using Business_Core.Entities.Identity.AdminAccount;
using Business_Core.Entities.Product;
using Bussiness_Core.Entities;
using Presentation.ViewModel;
using Presentation.ViewModel.CarouselViewModel.cs;
using Presentation.ViewModel.IdentityViewModel.AdminAccountBalance;
using Presentation.ViewModel.IdentityViewModel.User;
using Presentation.ViewModel.ProductsInDiscountDealsViewModel;
using Presentation.ViewModel.ProductViewModel;
using Presentation.ViewModels.Identity;
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
            CreateMap<Product, AddProductViewModel>().ReverseMap();
            CreateMap<GetProduct, GetProductForAdminViewModel>().ReverseMap();
            CreateMap<Product, GetProductForAdminViewModel>().ForMember(a=>a.ProductBrandName,
                opts => opts.MapFrom(src=>src.ProductBrand.BrandName)).ForMember(a=>a.NestCategoryName,
                opts => opts.MapFrom(src=>src.NestSubCategory.NestSubCategoryName));


            CreateMap<Product, UpdateProductViewModel>().ReverseMap();


            CreateMap<Carousel, CarouselViewModel>().ReverseMap();

            CreateMap<UserImage, PhotoForCreationViewModel>().ReverseMap();
            CreateMap<UserImage, PhotoForReturnViewModel>().ReverseMap();

            CreateMap<UserAddress, UserAddressViewModel>().ReverseMap();
            CreateMap<AdminAccount, AddAdminAccountBalanceViewModel>().ReverseMap();
            CreateMap<ProductDiscountDeal, ProductsUpdatedOnDiscountSaleViewModel>().ReverseMap();



        }
    }
}
