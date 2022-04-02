using AutoMapper;
using Business_Core.Entities;
using Business_Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Presentation.ViewModel;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicFormStructureController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDynamicFormStructureService _formStructureService;
        public DynamicFormStructureController(IMapper mapper, IDynamicFormStructureService formStructureService)
        {
            _mapper = mapper;
            _formStructureService = formStructureService;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetSingleFormStructure(int Id)
        {
            var useThisStringToSendDataToDatabase = "{'userId': 1,'id': 1,'title': 'sunt aut facere repellat provident occaecati excepturi optio reprehenderit','body': 'quia et suscipitnsuscipit recusandae consequuntur expedita et cumnreprehenderit molestiae ut ut quas totamnostrum rerumesautem sunt rem eveniet architecto'}";
            var detailData = await _formStructureService.GetDynamicFormStructure(Id);
            var convertingData = _mapper.Map<DynamicFormStructureViewModel>(detailData);
            return Ok(convertingData);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFormStructure()
        {

            var fullDetails = await _formStructureService.GetDynamicFormAllStructures();
            var convertingData = _mapper.Map<List<GetDynamicFormStructureViewModel>>(fullDetails);
            return Ok(convertingData);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSingleFormStructure(DynamicFormStructureViewModel viewModel)
        {
            var convertingModel = _mapper.Map<DynamicFormStructure>(viewModel);
            await _formStructureService.InsertDynamicFormStructure(convertingModel);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var findingData = await _formStructureService.GetDynamicFormStructure(Id);
            await _formStructureService.DeleteDynamicFormStructure(findingData);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(DynamicFormStructureViewModel viewModel)
        {
            var newData = _mapper.Map<DynamicFormStructure>(viewModel);
            var oldData = await _formStructureService.GetDynamicFormStructure(newData.DynamicFormStructureID);
            await _formStructureService.UpdateDynamicFormStructure(oldData, newData);
            return Ok();
        }
    }
}
