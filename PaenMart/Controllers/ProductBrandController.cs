using AutoMapper;
using Business_Core.Entities;
using Business_Core.Entities.Product;
using Business_Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModel;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBrandController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductBrandService _productBrandService;
        public ProductBrandController(IMapper mapper, IProductBrandService productBrandService)
        {
            _mapper = mapper;
            _productBrandService = productBrandService;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProductBrand(int Id)
        {
            var detailData = await _productBrandService.GetProductBrand(Id);
            var convertingData = _mapper.Map<ProductBrandViewModel>(detailData);
            return Ok(convertingData);
        }

        [HttpGet]
        public async Task<IActionResult> GeBrands()
        {
            var getDataFromServer = await _productBrandService.GetAllBrands();
            var convertingData = _mapper.Map<List<ProductBrandViewModel>>(getDataFromServer);
            return Ok(convertingData);
        }

        [HttpGet("GetAllProductBrandByNestSubCategory/{nestSubCategoryId}")]
        public async Task<IActionResult> GetAllProductBrandByNestSubCategory(int nestSubCategoryId)
        {
            var fullDetails = await _productBrandService.GetProductBrands(nestSubCategoryId);
            return Ok(fullDetails);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductBrand(ProductBrandViewModel viewModel)
        {
            var convertingModel = _mapper.Map<ProductBrand>(viewModel);
            await _productBrandService.InsertProductBrand(convertingModel);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var findingData = await _productBrandService.GetProductBrand(Id);
            await _productBrandService.DeleteProductBrand(findingData);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductBrandViewModel viewModel)
        {
            var newData = _mapper.Map<ProductBrand>(viewModel);
            var oldData = await _productBrandService.GetProductBrand(newData.ProductBrandID);
            await _productBrandService.UpdateProductBrand(oldData, newData);
            return Ok();
        }

        // NestSubCategoryProductBrand Table Crud

        [HttpPost("AddDataToNestSubProductBrand")]
        public async Task<IActionResult> AddDataToNestSubProductBrand(NestSubCategoryProductBrandViewModel viewModel)
        {
            var convertingModel = _mapper.Map<NestSubCategoryProductBrand>(viewModel);
            await _productBrandService.AddNestSubCategoryProductBrand(convertingModel);
            return Ok();
        }

        [HttpGet("GetAllProductsWithNestSubCategory")]

        public async Task<IActionResult> GetAllProductsWithNestSubCategory()
        {
            var gettingData = await  _productBrandService.GetAllNestSubAndProductBrands();
            return Ok(gettingData);
        }

        [HttpPut("DeleteNestSubAndProductBrand")]
        public async Task<IActionResult> DeleteNestSubAndProductBrand(NestSubCategoryProductBrandViewModel viewModelForIds)
        {
           await _productBrandService.DeleteDataFromNestAndBrand(viewModelForIds.NestSubCategoryId, viewModelForIds.ProductBrandId);
            return Ok();
        }






    }
}
