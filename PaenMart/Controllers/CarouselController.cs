using AutoMapper;
using Business_Core.Entities.Carousel;
using Business_Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModel.CarouselViewModel.cs;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarouselController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICarouselService _CarouselService;

        public CarouselController(IMapper mapper, ICarouselService CarouselService)
        {
            _mapper = mapper;
            _CarouselService = CarouselService;
        }

        [HttpPost]
        public async Task<IActionResult> AddingCarouselImage([FromForm] CarouselViewModel viewModel)
        {
            var convertingViewModel = _mapper.Map<Carousel>(viewModel);
            await _CarouselService.InsertCarousel(convertingViewModel, viewModel.File);
            return Created($"{Request.Scheme://request.host}{Request.Path}/{viewModel.CarouselID}", viewModel);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCarouselImage([FromForm] CarouselViewModel viewModel)
        {
            var newData = _mapper.Map<Carousel>(viewModel);
            var oldData = await _CarouselService.GetCarouselById(newData.CarouselID);
            await _CarouselService.UpdateCarousel(oldData, newData);
            return Created($"{Request.Scheme://request.host}{Request.Path}/{viewModel.CarouselID}", viewModel);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCarouselImage(int Id)
        {
            var findingData = await _CarouselService.GetCarouselById(Id);
            await _CarouselService.DeleteCarousel(findingData);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCarouselImages()
        {
            var gettingAllData = await _CarouselService.GetCarousel();
            var result = from s in gettingAllData
                         orderby s.ImagePriority
                         select s;
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetSingleCarouselImage(int Id)
        {
            var gettingAllData = await _CarouselService.GetCarouselById(Id);
            return Ok(gettingAllData);
        }
    }
}
