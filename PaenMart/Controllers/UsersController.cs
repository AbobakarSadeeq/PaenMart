using AutoMapper;
using Business_Core.Entities.Identity;
using Business_Core.Entities.Identity.user.Employee;
using Business_Core.Entities.Identity.user.Shipper;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModel.IdentityViewModel.User;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<CustomIdentity> _userManager;
        private readonly SignInManager<CustomIdentity> signInManger;

        public UsersController(DataContext dataContext,
            IMapper mapper, SignInManager<CustomIdentity> signInManger,
            RoleManager<IdentityRole> roleManager,
            UserManager<CustomIdentity> userManager)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
            this._roleManager = roleManager;
            this._userManager = userManager;
            this.signInManger = signInManger;

        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel viewModel)
        {
            if(viewModel.RoleName == "Employee")
            {

                var customIdentity = new CustomIdentity
                {
                    FullName = viewModel.FirstName + " " + viewModel.LastName,
                    UserName = viewModel.FirstName + viewModel.LastName,
                    Email = viewModel.Email
                };

                var findingUser = await _userManager.FindByNameAsync(viewModel.FirstName + viewModel.LastName);
                if (findingUser == null)
                {
                    customIdentity.FullName = viewModel.FirstName + " " + viewModel.LastName;
                    customIdentity.UserName = viewModel.FirstName + viewModel.LastName;
                }
                else
                {
                    return BadRequest($"UserName Already in Use {viewModel.FirstName + viewModel.LastName}");

                }
                var findingEmail = await _userManager.FindByEmailAsync(viewModel.Email);
                if (findingEmail == null)
                {
                    customIdentity.Email = viewModel.Email;
                }
                else
                {
                    return BadRequest($"Email Already in Use {viewModel.Email}");
                }

                var addingUser = await _userManager.CreateAsync(customIdentity, viewModel.UserPassword);
                IdentityResult result = null;
                if (addingUser.Succeeded)
                {
                    if (!await _userManager.IsInRoleAsync(customIdentity, viewModel.RoleName))
                    {
                        result = await _userManager.AddToRoleAsync(customIdentity, viewModel.RoleName);
                    }
                }

                // AddingEmployee
                var convertingData = new Employee
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    DathOfBirth = viewModel.DathOfBirth,
                    PhoneNumber = viewModel.PhoneNumber,
                    HomeAddress = viewModel.HomeAddress,
                    Salary = viewModel.Salary,
                    Gender = viewModel.Gender,
                    EmployeeHireDate = viewModel.HireDate,
                    UserId = customIdentity.Id,

                };

                await dataContext.Employees.AddAsync(convertingData);
                await dataContext.SaveChangesAsync();

                // Addning Employee Monthly Payment
                var employeePayment = new EmployeePayment
                {
                    Payment = false,
                    Payment_At = null,
                    EmployeeId = convertingData.EmployeeID
                };

                await dataContext.EmployeePayments.AddAsync(employeePayment);
                await dataContext.SaveChangesAsync();

            }
            else if(viewModel.RoleName == "Shipper")
            {
                var customIdentity = new CustomIdentity
                {
                    FullName = viewModel.FirstName + " " + viewModel.LastName,
                    UserName = viewModel.FirstName + viewModel.LastName,
                    Email = viewModel.Email
                };

                var findingUser = await _userManager.FindByNameAsync(viewModel.FirstName + viewModel.LastName);
                if (findingUser == null)
                {
                    customIdentity.FullName = viewModel.FirstName + " " + viewModel.LastName;
                    customIdentity.UserName = viewModel.FirstName + viewModel.LastName;
                }
                else
                {
                    return BadRequest($"UserName Already in Use {viewModel.FirstName + viewModel.LastName}");

                }
                var findingEmail = await _userManager.FindByEmailAsync(viewModel.Email);
                if (findingEmail == null)
                {
                    customIdentity.Email = viewModel.Email;
                }
                else
                {
                    return BadRequest($"Email Already in Use {viewModel.Email}");
                }

                var addingUser = await _userManager.CreateAsync(customIdentity, viewModel.UserPassword);
                IdentityResult result = null;
                if (addingUser.Succeeded)
                {
                    if (!await _userManager.IsInRoleAsync(customIdentity, viewModel.RoleName))
                    {
                        result = await _userManager.AddToRoleAsync(customIdentity, viewModel.RoleName);
                    }
                }

                // AddingEmployee
                var convertingData = new Shipper
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    DathOfBirth = viewModel.DathOfBirth,
                    PhoneNumber = viewModel.PhoneNumber,
                    HomeAddress = viewModel.HomeAddress,
                    Salary = viewModel.Salary,
                    Gender = viewModel.Gender,
                    ShipperHireDate = viewModel.HireDate,
                    UserId = customIdentity.Id,
                    VehiclePlatNo = viewModel.VehiclePlatNo,
                    ShipmentVehicleType = viewModel.ShipmentVehicleType,
                    NumberOfShipmentsDone = 0,
                };

                await dataContext.Shippers.AddAsync(convertingData);
                await dataContext.SaveChangesAsync();

                // Addning Employee Monthly Payment
                var shipperPayment = new ShipperPayment
                {
                    Payment = false,
                    Payment_At = null,
                    ShipperId = convertingData.ShipperID
                };

                await dataContext.ShipperPayments.AddAsync(shipperPayment);
                await dataContext.SaveChangesAsync();





            }
            else if (viewModel.RoleName == "User")
            {
                var customIdentity = new CustomIdentity
                {
                    FullName = viewModel.FirstName + " " + viewModel.LastName,
                    UserName = viewModel.FirstName + viewModel.LastName,
                    Email = viewModel.Email
                };

                var findingUser = await _userManager.FindByNameAsync(viewModel.FirstName + viewModel.LastName);
                if (findingUser == null)
                {
                    customIdentity.FullName = viewModel.FirstName + " " + viewModel.LastName;
                    customIdentity.UserName = viewModel.FirstName + viewModel.LastName;
                }
                else
                {
                    return BadRequest($"UserName Already in Use {viewModel.FirstName + viewModel.LastName}");

                }
                var findingEmail = await _userManager.FindByEmailAsync(viewModel.Email);
                if (findingEmail == null)
                {
                    customIdentity.Email = viewModel.Email;
                }
                else
                {
                    return BadRequest($"Email Already in Use {viewModel.Email}");
                }

                var addingUser = await _userManager.CreateAsync(customIdentity, viewModel.UserPassword);
                IdentityResult result = null;
                if (addingUser.Succeeded)
                {
                    if (!await _userManager.IsInRoleAsync(customIdentity, viewModel.RoleName))
                    {
                        result = await _userManager.AddToRoleAsync(customIdentity, viewModel.RoleName);
                    }
                }
            }
            else
            {
                return BadRequest("Something is going on wrong!");
            }
            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            var gettingId = await _userManager.FindByIdAsync(userId);
            await _userManager.DeleteAsync(gettingId);
            await dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var findingUserInEmployeeTable = await dataContext.Employees.Include(a=>a.User)
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if(findingUserInEmployeeTable != null)
            {
                var employeeViewModel = new
                {
                    EmployeeID = findingUserInEmployeeTable.EmployeeID,
                    UserId = findingUserInEmployeeTable.UserId,
                    FirstName = findingUserInEmployeeTable.FirstName,
                    LastName = findingUserInEmployeeTable.LastName,
                    DathOfBirth = findingUserInEmployeeTable.DathOfBirth,
                    PhoneNumber = findingUserInEmployeeTable.PhoneNumber,
                    HomeAddress = findingUserInEmployeeTable.HomeAddress,
                    Salary = findingUserInEmployeeTable.Salary,
                    Gender = findingUserInEmployeeTable.Gender,
                    EmployeeHireDate = findingUserInEmployeeTable.EmployeeHireDate,
                    Email = findingUserInEmployeeTable.User.Email
                };
                return Ok(employeeViewModel);

            }

            if (findingUserInEmployeeTable == null)
            {
                var findingUserInShipperTable = await dataContext.Shippers.Include(a=>a.User)
               .FirstOrDefaultAsync(a => a.UserId == userId);
                var shipperViewModel = new
                {
                    ShipperID = findingUserInShipperTable.ShipperID,
                    UserId = findingUserInShipperTable.UserId,
                    FirstName = findingUserInShipperTable.FirstName,
                    LastName = findingUserInShipperTable.LastName,
                    DathOfBirth = findingUserInShipperTable.DathOfBirth,
                    PhoneNumber = findingUserInShipperTable.PhoneNumber,
                    HomeAddress = findingUserInShipperTable.HomeAddress,
                    Salary = findingUserInShipperTable.Salary,
                    Gender = findingUserInShipperTable.Gender,
                    ShipperHireDate = findingUserInShipperTable.ShipperHireDate,
                    ShipmentVehicleType = findingUserInShipperTable.ShipmentVehicleType,
                    VehiclePlatNo = findingUserInShipperTable.VehiclePlatNo,
                    Email = findingUserInShipperTable.User.Email

                };
                return Ok(shipperViewModel);
            }

            return BadRequest();
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            
            var EmployeesList = await dataContext.Employees.Include(a => a.User).ToListAsync();
            return Ok(EmployeesList);
        }

        [HttpGet("GetAllShipper")]
        public async Task<IActionResult> GetAllShipper()
        {
            var ShipperList = await dataContext.Shippers.Include(a => a.User).ToListAsync();
            return Ok(ShipperList);
        }

        [HttpGet("GetAllUsers/{roleId}")]
        public async Task<IActionResult> GetAllUsers(string roleId)
        {
            var UserList = new List<GetUserViewModel>();
             var selectedUsers =  await dataContext.UserRoles
                .Where(a=>a.RoleId == roleId)
                .ToListAsync();
            foreach (var singleUser in selectedUsers)
            {
                var findUserData = await _userManager.FindByIdAsync(singleUser.UserId);
                UserList.Add(
                    new GetUserViewModel
                    {
                        RoleId = roleId,
                        UserID = singleUser.UserId,
                        RoleName = "User",
                        FullName = findUserData.FullName,
                        Email = findUserData.Email,
                    });
            }
            return Ok(UserList);
        }



        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserViewModel newData)
        {
            // update Employees data
            var findingEmployeeOldData = await dataContext.Employees
               .FirstOrDefaultAsync(a => a.UserId == newData.UserID);
            if (findingEmployeeOldData != null)
            {

                findingEmployeeOldData.DathOfBirth = newData.DathOfBirth;
                findingEmployeeOldData.PhoneNumber = newData.PhoneNumber;
                findingEmployeeOldData.HomeAddress = newData.HomeAddress;
                findingEmployeeOldData.Salary = newData.Salary;
                findingEmployeeOldData.Gender = newData.Gender;
                findingEmployeeOldData.EmployeeHireDate = newData.HireDate;
                await dataContext.SaveChangesAsync();
                return Ok();

            }

            // update Shipper data
            if (findingEmployeeOldData == null)
            {
                var findingShipperOldData = await dataContext.Shippers
               .FirstOrDefaultAsync(a => a.UserId == newData.UserID);

                findingShipperOldData.DathOfBirth = newData.DathOfBirth;
                findingShipperOldData.PhoneNumber = newData.PhoneNumber;
                findingShipperOldData.HomeAddress = newData.HomeAddress;
                findingShipperOldData.Salary = newData.Salary;
                findingShipperOldData.Gender = newData.Gender;
                findingShipperOldData.ShipperHireDate = newData.HireDate;
                findingShipperOldData.VehiclePlatNo = newData.VehiclePlatNo;
                findingShipperOldData.ShipmentVehicleType = newData.ShipmentVehicleType;
                await dataContext.SaveChangesAsync();


            }




            return Ok();
        }
    }
}
