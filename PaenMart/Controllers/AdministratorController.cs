using Business_Core.Entities.Identity;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModels.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_Project_Levi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<CustomIdentity> _userManager;
        private readonly DataContext _dataContext;

        public AdministratorController(DataContext dataContext, RoleManager<IdentityRole> roleManager, UserManager<CustomIdentity> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            _dataContext = dataContext;

        }

        // Adding Role or when Admin want to add another role this method will invoke in api

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createViewModel)
        {
            IdentityRole identityRole = new IdentityRole();

            var findingRole = await _roleManager.FindByNameAsync(createViewModel.RoleName);

            if (findingRole == null)
            {
                identityRole.Name = createViewModel.RoleName;
            }
            else
            {
                return BadRequest($"Role Already in Use {createViewModel.RoleName} ");
            }

            var Result = await _roleManager.CreateAsync(identityRole);
            if (Result.Succeeded)
            {
                return Created($"{Request.Scheme://request.host}{Request.Path}/{createViewModel.Id}", createViewModel);

            }
            return BadRequest(Result.Errors);

        }

        //Getting List of Roles from server
        [HttpGet]
        public IActionResult ListRole()
        {
            var roles = _roleManager.Roles; //Role property have already Iqueryable generic list so, don't need to write or put in list
            return Ok(roles);
        }

        // Get data by Id or single Role Data.
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetDataforEdit(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);

            if (role == null)
            {
                return BadRequest($"Data is not Found {Id} please Try again!");

            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            return Ok(model);
        }

        // Admin Can Remove the Entire role from Database
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return BadRequest($"Data is not Found {roleId} please Try again!");
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role);
                return Ok();
            }
        }

        [HttpPut("EditRole")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                return BadRequest($"Data is not Found {model.Id} please Try again!");

            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return Created($"{Request.Scheme://request.host}{Request.Path}/{model.Id}", model);
                }
                else
                {
                    return BadRequest($"Role Already in Use {model.RoleName} ");

                }
            }
        }

        // getting single role all Users who's are inside that role or not show all Users and send it back to the client side
        [HttpGet("EditUserInRole/{roleId}")]
        public async Task<IActionResult> EditUserInRole(string roleId)
        {
            //find the role if it is found then do else statment.
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return BadRequest($"Data is not Found {roleId} please Try again!");
            }

            var allUserDataShow = new List<UserRoleViewModel>();

            //getting the all user data  and store it in the list UserRoleViewModel
            foreach (var singleUserData in _userManager.Users)
            {
                //adding those data which i want to show or give to api.
                var addingUserDataSpecific = new UserRoleViewModel
                {
                    UserId = singleUserData.Id,
                    UserEmail = singleUserData.Email

                };

                //finding that is any user have any kind of role
                if (await _userManager.IsInRoleAsync(singleUserData, role.Name))
                {
                    addingUserDataSpecific.IsSelected = true;
                }
                else
                {
                    addingUserDataSpecific.IsSelected = false;
                }
                //Now adding that data to the list of UserRole
                allUserDataShow.Add(addingUserDataSpecific);
            }

            return Ok(allUserDataShow);
        }

        [HttpPost("updateRoleUser/{roleId}")]
        public async Task<IActionResult> updateRoleUser(string roleId, [FromForm] List<UserRoleViewModel> model)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return BadRequest($"Data is not Found {roleId} please Try again!");
            }
            foreach (var item in model)
            {
                var user = await _userManager.FindByIdAsync(item.UserId);
                IdentityResult result = null;
                if (item.IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);

                    // When new user added to the roll and old one is removed then update the account Id also
                    if (role.Name == "Admin" || role.Name == "ADMIN")
                    {
                        //var getLastData = await _dataContext.AccountBalances.OrderByDescending(a => a.BalanceAccountId)
                        //.FirstOrDefaultAsync();
                        //getLastData.User_ID = item.UserId;
                        //getLastData.Modified_At = DateTime.Now;
                        //await _dataContext.SaveChangesAsync();
                    }


                }
                else if (!item.IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }

            }
            return Created($"{Request.Scheme://request.host}{Request.Path}/{roleId}", model);
        }

    }
}
