using Business_Core.Entities.Identity.UserAddress;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public CountryController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var countryList = await _dataContext.Countries.ToListAsync();
            return Ok(countryList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCountryById(int Id)
        {
            var singleCountry = await _dataContext.Countries.FirstOrDefaultAsync(a => a.CountryID == Id);
            return Ok(singleCountry);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCountryById(int Id)
        {
            var findingSelectedId = await _dataContext.Countries.FirstOrDefaultAsync(a=>a.CountryID == Id);
            _dataContext.Countries.Remove(findingSelectedId);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddCountry(Country country)
        {
            await _dataContext.Countries.AddAsync(country);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

    }
}
