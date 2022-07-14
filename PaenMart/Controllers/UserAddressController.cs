using AutoMapper;
using Bussiness_Core.Entities;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModel.IdentityViewModel.User;
using Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_Project_Levi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        public UserAddressController(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUserAddress(string UserId)
        {
            var gettingData = await dataContext.UserAddresses
                .Include(a => a.City)
                .Include(a => a.City.Country)
                .SingleOrDefaultAsync(a => a.UserId == UserId);
            if (gettingData == null)
            {
                return Ok(true);
            }
            var convertViewModel = mapper.Map<UserAddressViewModel>(gettingData);
            return Ok(convertViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUserAddress( UserAddressViewModel viewModel)
        {
            var convertingViewModel = mapper.Map<UserAddress>(viewModel);
            await dataContext.UserAddresses.AddAsync(convertingViewModel);
            await dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditUserAddress([FromForm] UserAddressViewModel viewModel)
        {
            var oldData = await dataContext.UserAddresses.SingleOrDefaultAsync(a => a.UserId == viewModel.UserId);
            oldData.PhoneNumber = viewModel.PhoneNumber;
            oldData.CompleteAddress = viewModel.CompleteAddress;
            oldData.CityId = viewModel.CityId;
            oldData.UserId = oldData.UserId;
            await dataContext.SaveChangesAsync();
            return Ok();
        }
    }
}
