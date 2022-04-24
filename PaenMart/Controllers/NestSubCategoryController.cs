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
    public class NestSubCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INestSubCategoryService _nestSubCategoryService;
        public NestSubCategoryController(IMapper mapper, INestSubCategoryService subCategoryService)
        {
            _mapper = mapper;
            _nestSubCategoryService = subCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNestSubCategories()
        {
            var gettingListOfNestSubCategory = await _nestSubCategoryService.GetNestSubCategories();
            var convertingData = _mapper.Map<List<GetNestSubCategoryViewModel>>(gettingListOfNestSubCategory);
            return Ok(convertingData);
        }


        [HttpGet("{singleNestSubCategoryId}")]
        public async Task<IActionResult> GetSingleNestSubCategory(int singleNestSubCategoryId)
        {
            var fullDetails = await _nestSubCategoryService.GetNestSubCategory(singleNestSubCategoryId);
             var convertingData = _mapper.Map<NestSubCategoryViewModel>(fullDetails);
            return Ok(convertingData);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNestSubCategory(NestSubCategoryViewModel viewModel)
        {
            var convertingModel = _mapper.Map<NestSubCategory>(viewModel);
            await _nestSubCategoryService.InsertNestSubCategory(convertingModel);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var findingData = await _nestSubCategoryService.GetNestSubCategory(Id);
            await _nestSubCategoryService.DeleteNestSubCategory(findingData);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(NestSubCategoryViewModel viewModel)
        {
            var newData = _mapper.Map<NestSubCategory>(viewModel);
            var oldData = await _nestSubCategoryService.GetNestSubCategory(newData.NestSubCategoryID);
            await _nestSubCategoryService.UpdateNestSubCategory(oldData, newData);
            return Ok();
        }
    }
}
