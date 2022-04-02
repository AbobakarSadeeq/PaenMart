using AutoMapper;
using Business_Core.Entities;
using Business_Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModel;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISubCategoryService _subCategoryService;
        public SubCategoryController(IMapper mapper, ISubCategoryService subCategoryService)
        {
            _mapper = mapper;
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubCategoryForTable()
        {
            var fullDetails = await _subCategoryService.GetSubCategoriesForTable();
            var convertingData = _mapper.Map<List<GetSubCategoriesViewModel>>(fullDetails);
            return Ok(convertingData);
        }

        [HttpGet("GetSingleSubCategory/{singleSubCategoryId}")]

        public async Task<IActionResult> GetSingleSubCategory(int singleSubCategoryId)
        {
            var getSubCategoryData = await _subCategoryService.GetSubCategory(singleSubCategoryId);
            var convertData = _mapper.Map<SubCategoryViewModel>(getSubCategoryData);
            return Ok(convertData);
        }


        [HttpGet("{singleCategoryId}")]
        public async Task<IActionResult> GetAllSubCategory(int singleCategoryId)
        {
            var fullDetails = await _subCategoryService.GetSubCategories(singleCategoryId);
            var convertingData = _mapper.Map<List<SubCategoryViewModel>>(fullDetails);
            return Ok(convertingData);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubCategory(SubCategoryViewModel viewModel)
        {
            var convertingModel = _mapper.Map<SubCategory>(viewModel);
            await _subCategoryService.InsertSubCategory(convertingModel);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var findingData = await _subCategoryService.GetSubCategory(Id);
            await _subCategoryService.DeleteSubCategory(findingData);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(SubCategoryViewModel viewModel)
        {
            var newData = _mapper.Map<SubCategory>(viewModel);
            var oldData = await _subCategoryService.GetSubCategory(newData.SubCategoryID);
            await _subCategoryService.UpdateSubCategory(oldData, newData);
            return Ok();
        }
    }
}
