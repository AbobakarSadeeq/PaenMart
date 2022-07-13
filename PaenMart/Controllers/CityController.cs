using Business_Core.Entities.Identity.UserAddress;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModel.IdentityViewModel.User;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public CityController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var cityList = await _dataContext.Cities.Include(a=>a.Country).ToListAsync();
            var list = new List<CityViewModel>();
            foreach (var singleCity in cityList)
            {
                list.Add(new CityViewModel
                { 
                    CityName = singleCity.CityName,
                    CountryName = singleCity.Country.CountryName,
                    CityID = singleCity.CityID,
                    CountryId = singleCity.CountryId
                });
            }
            return Ok(list);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCityById(int Id)
        {
            var singleCity = await _dataContext
                .Cities
                .Include(a => a.Country)
                .FirstOrDefaultAsync(a => a.CityID == Id);

            var viewModel = new CityViewModel
            {
                CityID = singleCity.CityID,
                CountryId = singleCity.CountryId,
                CountryName = singleCity.Country.CountryName,
                CityName = singleCity.CityName
            };

            return Ok(viewModel);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCityById(int Id)
        {
            var findingSelectedId = await _dataContext.Cities.FirstOrDefaultAsync(a => a.CityID == Id);
            _dataContext.Cities.Remove(findingSelectedId);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Addcity(City city)
        {
            await _dataContext.Cities.AddAsync(city);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}
