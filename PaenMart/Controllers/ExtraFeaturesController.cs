using Business_Core.Entities.Identity.Email;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtraFeaturesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ExtraFeaturesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetOwnerEmailDetail()
        {
            var getEmailInfo = await _dataContext.SendingEmails.FirstOrDefaultAsync();
            return Ok(getEmailInfo);
        }

        [HttpPost]
        public async Task<IActionResult> PostOwnerEmailDetail(SendingEmail sendingEmail)
        {
            await _dataContext.SendingEmails.AddAsync(sendingEmail);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOwnerEmailDetail(SendingEmail newData)
        {
            var findingEmailInfo = await _dataContext.SendingEmails.FirstOrDefaultAsync();
            findingEmailInfo.OwnerEmail = newData.OwnerEmail;
            findingEmailInfo.AppPassword = newData.AppPassword;
            await _dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}
