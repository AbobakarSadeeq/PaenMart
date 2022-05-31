using AutoMapper;
using Business_Core.Entities.Product;
using Business_Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModel.ProductViewModel;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public ProductController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpPost]
        
        public async Task<IActionResult> AddProduct([FromForm] AddProductViewModel viewModel)
        {
            var convertingModel = _mapper.Map<Product>(viewModel);
            await _productService.InsertProduct(convertingModel, viewModel.File);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductViewModel viewModel)
        {
            var newData = _mapper.Map<Product>(viewModel);
            var oldData = await _productService.GetProduct(newData.ProductID);
            await _productService.UpdateProduct(oldData, newData);

            // if addedd more new images then 
            if(viewModel.File != null)
            {
                _productService.UpdateProductImages(viewModel.ProductID, viewModel.File);
            }

            return Ok();
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteProduct(int Id)
        {
            _productService.DeleteProductData(Id);
            return Ok();
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProduct(int Id)
        {
            var detailData = await _productService.GetSingleProduct(Id);
            return Ok(detailData);
        }


        // getAll products with its brand and with its nest-sub-category to show in list and show that data only in admin getproducts table.
        [HttpGet("GetSelectedCategoryProducts")]
        public async Task<IActionResult> GetSelectedCategoryProducts([FromQuery] PageSelectedAndNestCategoryId nestCategoryIdAndPageSelected)
        {  
            var productList = await _productService.GetProducts(nestCategoryIdAndPageSelected);
            var convertProductData = _mapper.Map<List<GetProductForAdminViewModel>>(productList);
            int countProductsRows = nestCategoryIdAndPageSelected.singleCategoryTotalProductsCount;
            return Ok(new {productData = convertProductData, countProducts = countProductsRows });
        }

        // get the products by brand, when clicked on brand then get those whose are related.
        [HttpGet("GetProductsByBrand/{brandId}")]
        public async Task<IActionResult> GetProductsByBrand(int brandId)
        {
            var detailData = await _productService.GetProductsByBrandId(brandId);
            var convertProductData = _mapper.Map<List<GetProductViewModel>>(detailData);
            return Ok(convertProductData);
        }

        // get the product by nest category, when clicked on category then get those whose are related.
        [HttpGet("GetProductsByNestSubCategory/{NestCategoryId}")]
        public async Task<IActionResult> GetProductsByNestSubCategory(int NestCategoryId)
        {
            var detailData = await _productService.GetProductsByNestSubCategoryId(NestCategoryId);
            var convertProductData = _mapper.Map<List<GetProductViewModel>>(detailData);
            return Ok(convertProductData);
        }


        [HttpDelete("DeleteSingleProductSingleImage/{ImageId}")]
        public IActionResult DeleteSingleProductSingleImage(string ImageId)
        {
            _productService.DeletingSingleImageProduct(ImageId);
            return Ok();
        }

    }
}

 