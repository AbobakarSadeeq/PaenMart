using Business_Core.IServices;
using Bussiness_Core.IServices;
using Data_Access.Services_Implement;
using DataAccess.Data.Services_Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Extensions
{
    public static class ServiceLayerServiceConfigurationExtension
    {
        public static void ConfigureServiceLayer(this IServiceCollection services)
        {
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ISubCategoryService, SubCategoryService>();
            services.AddTransient<INestSubCategoryService, NestSubCategoryService>();
            services.AddTransient<IProductBrandService, ProductBrandService>();
            services.AddTransient<IDynamicFormStructureService, DynamicFormStructureService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICarouselService, CarouselService>();
            services.AddTransient<IUserPhotoService, UserPhotoService>();


        }

    }
}
