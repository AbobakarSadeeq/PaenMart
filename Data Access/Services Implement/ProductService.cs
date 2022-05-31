using Business_Core.Entities.Product;
using Business_Core.Entities.Product.Product_Images;
using Business_Core.IServices;
using Business_Core.IUnitOfWork;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Services_Implement
{
    public class ProductService : IProductService
    {
        private readonly IUnitofWork _unitofWork;
        public ProductService(IUnitofWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }
        public void DeleteProductData(int Id)
        {
            _unitofWork._ProductRepository.DeleteProduct(Id);
             _unitofWork.CommitAsync();
        }

        public async Task<IEnumerable<Product>> GetProduct()
        {
            return await _unitofWork._ProductRepository.GetAllSync();
        }

        public async Task<Product> GetProduct(int Id)
        {
            return await _unitofWork._ProductRepository.GetByKeyAsync(Id);
        }

        public async Task<Product> InsertProduct(Product product, List<IFormFile> File)
        {
            product.Created_At = DateTime.Now;
            var getImagesForAdding = _unitofWork._ProductRepository.AddProductImage(File);
            product.ProductImages = getImagesForAdding;
            await _unitofWork._ProductRepository.AddAsync(product);
            await _unitofWork.CommitAsync();
            
            return product;
        }

        public async Task<Product> UpdateProduct(Product OldData, Product UpdateData)
        {
            OldData.ProductName = UpdateData.ProductName;
            OldData.Color = UpdateData.Color;
            OldData.Price = UpdateData.Price;
            OldData.StockAvailiability = UpdateData.StockAvailiability;
            OldData.SellUnits = OldData.SellUnits;
            OldData.Quantity = UpdateData.Quantity;
            OldData.ProductDetails = UpdateData.ProductDetails;
            OldData.ProductBrandId = UpdateData.ProductBrandId;
            OldData.NestSubCategoryId = UpdateData.NestSubCategoryId;
            OldData.Modified_at = DateTime.Now;
            await _unitofWork.CommitAsync();
            return OldData;
        }

        // get by Id for detail page of each product and get by Id for admin page details with both table joining brand and nestsubcategory.

        public async Task<GetProduct> GetSingleProduct(int Id)
        {
            return await _unitofWork._ProductRepository.GetProductById(Id);
        }



        public async Task<IEnumerable<Product>> GetProducts(PageSelectedAndNestCategoryId pageSelectedAndNestCategoryId)
        {
            return await _unitofWork._ProductRepository.GetAll(pageSelectedAndNestCategoryId);
        }

        public async Task<IEnumerable<GetProduct>> GetProductsByBrandId(int brandId)
        {
            return await _unitofWork._ProductRepository.GetAllProductsByBrand(brandId);
        }

        public async Task<IEnumerable<GetProduct>> GetProductsByNestSubCategoryId(int NestCategoryId)
        {
            return await _unitofWork._ProductRepository.GetAllProductsByNestSubCategory(NestCategoryId);
        }

        public  void DeletingSingleImageProduct(string publicId)
        {
             _unitofWork._ProductRepository.DeleteSingleImageOfProduct(publicId);
        }

        public void UpdateProductImages(int productId, List<IFormFile> File)
        {
            _unitofWork._ProductRepository.UpdateProductImage(productId, File);
            _unitofWork.CommitAsync();
        }
    }
}
