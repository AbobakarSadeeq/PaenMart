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
        
        public async Task<IActionResult> AddProduct(ProductViewModel viewModel)
        {
            var convertingModel = _mapper.Map<Product>(viewModel);
            await _productService.InsertProduct(convertingModel);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductViewModel viewModel)
        {
            var newData = _mapper.Map<Product>(viewModel);
            var oldData = await _productService.GetProduct(newData.ProductID);
            await _productService.UpdateProduct(oldData, newData);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            var findingData = await _productService.GetProduct(Id);
            await _productService.DeleteProduct(findingData);
            return Ok();
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProduct(int Id)
        {
            var detailData = await _productService.GetSingleProduct(Id);
            return Ok(detailData);
        }

        // getAll products with its brand and with its nest-sub-category to show in list and show that data only in admin getproducts table.
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var productList = await _productService.GetProducts();
            return Ok(productList);
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

    }
}

 