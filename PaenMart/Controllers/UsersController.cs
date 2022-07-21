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
using Presentation.ViewModel.IdentityViewModel.User.Employee;
using Presentation.ViewModel.IdentityViewModel.User.Shipper;
using System.Net;
using System.Net.Mail;

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
                    PaymentStatus = "Pending",
                    PaymentHistory = false,
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
                    PaymentStatus = "Pending",
                    PaymentHistory = false,
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
            var EmployeeUserList = new List<GetEmployeeShipperViewModel>();
            var EmployeesList = await dataContext.Employees
                .Include(a => a.User)
                .ToListAsync();

            foreach (var item in EmployeesList)
            {
                EmployeeUserList.Add(new GetEmployeeShipperViewModel
                {
                    HomeAddress = item.HomeAddress,
                    Salary = item.Salary,
                    DathOfBirth = item.DathOfBirth,
                    Email = item.User.Email,
                    HireDate = item.EmployeeHireDate,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Gender = item.Gender,
                    ID = item.EmployeeID,
                    userId = item.UserId,
                    RoleName = "Employee",
                    PhoneNumber = item.PhoneNumber,
                });
            }

            return Ok(EmployeeUserList);
        }

        [HttpGet("GetAllShipper")]
        public async Task<IActionResult> GetAllShipper()
        {
            var ShipperUserList = new List<GetEmployeeShipperViewModel>();
            var ShipperList = await dataContext.Shippers
                .Include(a => a.User)
                .ToListAsync();
            foreach (var item in ShipperList)
            {
                ShipperUserList.Add(new GetEmployeeShipperViewModel
                {
                    HomeAddress = item.HomeAddress,
                    Salary = item.Salary,
                    DathOfBirth = item.DathOfBirth,
                    Email = item.User.Email,
                    HireDate = item.ShipperHireDate,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Gender = item.Gender,
                    ID = item.ShipperID,
                    userId = item.UserId,
                    RoleName = "Shipper",
                    PhoneNumber = item.PhoneNumber,
                    ShipmentVehicleType = item.ShipmentVehicleType,
                    VehiclePlatNo = item.VehiclePlatNo,
                });
            }
            return Ok(ShipperUserList);
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

        // Payment API's of employees

        [HttpPut("PayingEmployeeMonthlyPayment/{employeePaymentId}")]
        public async Task<IActionResult> PayingEmployeeMonthlyPayment(int employeePaymentId)
        {
            var findingEmployeePaymentData = await dataContext.EmployeePayments
                .FirstOrDefaultAsync(a=>a.EmployeeMonthlyPaymentID == employeePaymentId);
           

            // getting employees details
            var findingEmployeeDetailsData = await dataContext.Employees
                .FirstOrDefaultAsync(a=>a.EmployeeID == findingEmployeePaymentData.EmployeeId);

            // cut the employee salary from admin account
            var adminAccountDetails = await dataContext.AdminAccounts.ToListAsync();
            var takeLastAccountTransactionDetails = adminAccountDetails.LastOrDefault();

            if(takeLastAccountTransactionDetails.CurrentBalance >= findingEmployeeDetailsData.Salary)
            {
                takeLastAccountTransactionDetails.BeforeBalance = takeLastAccountTransactionDetails.CurrentBalance;
                takeLastAccountTransactionDetails.TransactionPurpose = "Employee monthly payment cut";
                takeLastAccountTransactionDetails.TransactionDateTime = DateTime.Now;
                takeLastAccountTransactionDetails.BalanceSituation = "Subtract";
                takeLastAccountTransactionDetails.AdminAccountID = 0;
                takeLastAccountTransactionDetails.CurrentBalance = takeLastAccountTransactionDetails.CurrentBalance - findingEmployeeDetailsData.Salary;

                findingEmployeePaymentData.Payment = true;
                findingEmployeePaymentData.Payment_At = DateTime.Now;
                findingEmployeePaymentData.PaymentStatus = "Employee-Paid";
                findingEmployeePaymentData.PaymentHistory = true;


                await dataContext.AdminAccounts.AddAsync(takeLastAccountTransactionDetails);
                await dataContext.SaveChangesAsync();
            }else
            {
                return BadRequest("Sorry account does not have a required balance to pay employee salary, Please recharge");
            }

            // sending email

            var findUserData = await _userManager.FindByIdAsync(findingEmployeeDetailsData.UserId);
            var getEmailSendEmailData = await dataContext.SendingEmails.FirstOrDefaultAsync();

            var msgObj = new MailMessage(getEmailSendEmailData.OwnerEmail, findUserData.Email);
            msgObj.Subject = "Monthly Salary Payment";
            msgObj.IsBodyHtml = true;
            msgObj.Body = @$"
                        <h2>Monthly Salary Payment</h2>
                        <p>Dear, <strong>{findingEmployeeDetailsData.FirstName + " " + findingEmployeeDetailsData.LastName}</strong></p>
                        <p> Your monthly salary has been paid by your client <strong> PKR{findingEmployeeDetailsData.Salary},</strong> at  <strong> {findingEmployeePaymentData.Payment_At.Value.ToString("F")} </strong> </p>
                        <p>Thank you</p><hr>"
                        ;

            SmtpClient clientData = new SmtpClient("smtp.gmail.com", 587);
            clientData.EnableSsl = true;
            clientData.DeliveryMethod = SmtpDeliveryMethod.Network;
            clientData.UseDefaultCredentials = false;
            clientData.Credentials = new NetworkCredential() { UserName = getEmailSendEmailData.OwnerEmail, Password = getEmailSendEmailData.AppPassword }; // write that email which is store in database
            clientData.Send(msgObj);


            return Ok();
        }

        [HttpPut("PayingEmployeeMonthlyPaymentAgainApplying/{employeePaymentId}")]
        public async Task<IActionResult> PayingEmployeeMonthlyPaymentAgainApplying(int employeePaymentId)
        {
            var findingEmployeeData = await dataContext.EmployeePayments
              .FirstOrDefaultAsync(a => a.EmployeeMonthlyPaymentID == employeePaymentId);
            findingEmployeeData.PaymentStatus = "Payment-Done";

            EmployeePayment newEmployeePaymentEntry = new EmployeePayment
            {
                EmployeeMonthlyPaymentID = 0,
                PaymentStatus = "Pending",
                Payment_At = null,
                Payment = false,
                EmployeeId = findingEmployeeData.EmployeeId
            };
            await dataContext.EmployeePayments.AddAsync(newEmployeePaymentEntry);
            //findingEmployeeData.Payment_At = DateTime.Now; when paying the employee payment then show the live dateTime in front-end
            await dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("GetEmployeesPendingPayment")]
        public async Task<IActionResult> GetEmployeesPendingPayment()
        {
            var convertViewModelEmployeePayment = new List<GetEmployeesPayment>();

            var gettingUnPaidEmployeeList = await dataContext.EmployeePayments.Include(a=>a.Employee)
                .Where(a => a.PaymentStatus == "Pending")
                .ToListAsync();
            foreach(var empData in gettingUnPaidEmployeeList)
            {
                convertViewModelEmployeePayment.Add(new GetEmployeesPayment
                {
                    FullName = empData.Employee.FirstName + " " + empData.Employee.LastName,
                    PhoneNumber = empData.Employee.PhoneNumber,
                    Payment_At = empData.Payment_At,
                    Salary = empData.Employee.Salary,
                    EmployeePaymentId = empData.EmployeeMonthlyPaymentID,
                    PaymentStatus = empData.PaymentStatus

                });
            }

            return Ok(convertViewModelEmployeePayment);
        }

        [HttpGet("PaidEmployeesList")]
        public async Task<IActionResult> PaidEmployeesList()
        {
            var convertViewModelEmployeePayment = new List<GetEmployeesPayment>();

            var gettingPaidEmployeesList = await dataContext.EmployeePayments
                .Include(a => a.Employee)
                .Where(a => a.PaymentStatus == "Employee-Paid")
                .ToListAsync();
            foreach (var empData in gettingPaidEmployeesList)
            {
                convertViewModelEmployeePayment.Add(new GetEmployeesPayment
                {
                    FullName = empData.Employee.FirstName + " " + empData.Employee.LastName,
                    PhoneNumber = empData.Employee.PhoneNumber,
                    Payment_At = empData.Payment_At,
                    Salary = empData.Employee.Salary,
                    EmployeePaymentId = empData.EmployeeMonthlyPaymentID,
                    PaymentStatus = empData.PaymentStatus
                });
            }

            return Ok(convertViewModelEmployeePayment);
        }

        [HttpGet("EmployeePaymentHistory/{pageNo}")]
        public async Task<IActionResult> EmployeePaymentHistory(int pageNo)
        {
            var convertViewModelEmployeePayment = new List<GetEmployeesPayment>();
            var gettingPaidEmployeesList = await dataContext.EmployeePayments
                .Include(a => a.Employee)
                .Where(a => a.PaymentHistory == true)
                .ToListAsync();
            foreach (var empPayments in gettingPaidEmployeesList)
            {
                convertViewModelEmployeePayment.Add(new GetEmployeesPayment
                {
                    EmployeeID = empPayments.EmployeeId,
                    FullName = empPayments.Employee.FirstName + " " + empPayments.Employee.LastName,
                    PaymentStatus = empPayments.PaymentStatus,
                    Payment_At = empPayments.Payment_At,
                    PhoneNumber = empPayments.Employee.PhoneNumber,
                    Salary = empPayments.Employee.Salary
                });
            }

            if (pageNo == 1)
            {

                var firstPage = convertViewModelEmployeePayment.Take(12);
                return Ok(new
                {
                    employeePaymentData = firstPage,
                   Count =  convertViewModelEmployeePayment.Count
                });

            }

            int skipPageSize = (pageNo - 1) * 12;
            var otherPages = convertViewModelEmployeePayment.Skip(skipPageSize).Take(12);
            return Ok(new
            {
                employeePaymentData = otherPages,
                Count = convertViewModelEmployeePayment.Count
            });
        }


        // --------------------- Shipper Payment ----------------------------

        [HttpPut("PayingShipperMonthlyPayment/{shipperPaymentId}")]
        public async Task<IActionResult> PayingShipperMonthlyPayment(int shipperPaymentId)
        {
            var findingShipperPaymentData = await dataContext.ShipperPayments
                .FirstOrDefaultAsync(a => a.ShipperMonthlyPaymentID == shipperPaymentId);


            // getting shipper details
            var findingShipperDetailsData = await dataContext.Shippers
                .FirstOrDefaultAsync(a => a.ShipperID == findingShipperPaymentData.ShipperId);

            // cut the shipper salary from admin account
            var adminAccountDetails = await dataContext.AdminAccounts.ToListAsync();
            var takeLastAccountTransactionDetails = adminAccountDetails.LastOrDefault();

            if (takeLastAccountTransactionDetails.CurrentBalance >= findingShipperDetailsData.Salary)
            {
                takeLastAccountTransactionDetails.BeforeBalance = takeLastAccountTransactionDetails.CurrentBalance;
                takeLastAccountTransactionDetails.TransactionPurpose = "Shipper monthly payment cut";
                takeLastAccountTransactionDetails.TransactionDateTime = DateTime.Now;
                takeLastAccountTransactionDetails.BalanceSituation = "Subtract";
                takeLastAccountTransactionDetails.AdminAccountID = 0;
                takeLastAccountTransactionDetails.CurrentBalance = takeLastAccountTransactionDetails.CurrentBalance - findingShipperDetailsData.Salary;

                findingShipperPaymentData.Payment = true;
                findingShipperPaymentData.Payment_At = DateTime.Now;
                findingShipperPaymentData.PaymentStatus = "Shipper-Paid";
                findingShipperPaymentData.PaymentHistory = true;


                await dataContext.AdminAccounts.AddAsync(takeLastAccountTransactionDetails);
                await dataContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Sorry account does not have a required balance to pay shipper salary, Please recharge");
            }

            var findUserData = await _userManager.FindByIdAsync(findingShipperDetailsData.UserId);
            var getEmailSendEmailData = await dataContext.SendingEmails.FirstOrDefaultAsync();

            var msgObj = new MailMessage(getEmailSendEmailData.OwnerEmail, findUserData.Email);
            msgObj.Subject = "Monthly Salary Payment";
            msgObj.IsBodyHtml = true;
            msgObj.Body = @$"
                        <h2>Monthly Salary Payment</h2>
                        <p>Dear, <strong>{findingShipperDetailsData.FirstName + " " + findingShipperDetailsData.LastName}</strong></p>
                        <p> Your monthly salary has been paid by your client <strong> PKR{findingShipperDetailsData.Salary},</strong> at  <strong> {findingShipperPaymentData.Payment_At.Value.ToString("F")} </strong> </p>
                        <p><strong>Thank you</strong></p><hr>"
                        ;

            SmtpClient clientData = new SmtpClient("smtp.gmail.com", 587);
            clientData.EnableSsl = true;
            clientData.DeliveryMethod = SmtpDeliveryMethod.Network;
            clientData.UseDefaultCredentials = false;
            clientData.Credentials = new NetworkCredential() { UserName = getEmailSendEmailData.OwnerEmail, Password = getEmailSendEmailData.AppPassword }; // write that email which is store in database
            clientData.Send(msgObj);


            return Ok();
        }

        [HttpPut("PayingShipperMonthlyPaymentAgainApplying/{shipperPaymentId}")]
        public async Task<IActionResult> PayingShipperMonthlyPaymentAgainApplying(int shipperPaymentId)
        {
            var findingShipperData = await dataContext.ShipperPayments
              .FirstOrDefaultAsync(a => a.ShipperMonthlyPaymentID == shipperPaymentId);
            findingShipperData.PaymentStatus = "Payment-Done";

            ShipperPayment newShipperPaymentEntry = new ShipperPayment
            {
                ShipperMonthlyPaymentID = 0,
                PaymentStatus = "Pending",
                Payment_At = null,
                Payment = false,
                ShipperId = findingShipperData.ShipperId
            };
            await dataContext.ShipperPayments.AddAsync(newShipperPaymentEntry);
            await dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("GetShipperPendingPayment")]
        public async Task<IActionResult> GetShipperPendingPayment()
        {
            var convertViewModelShipperPayment = new List<GetShipperPaymentViewModel>();

            var gettingUnPaidShipperList = await dataContext.ShipperPayments.Include(a => a.Shipper)
                .Where(a => a.PaymentStatus == "Pending")
                .ToListAsync();
            foreach (var shipperData in gettingUnPaidShipperList)
            {
                convertViewModelShipperPayment.Add(new GetShipperPaymentViewModel
                {
                    FullName = shipperData.Shipper.FirstName + " " + shipperData.Shipper.LastName,
                    PhoneNumber = shipperData.Shipper.PhoneNumber,
                    Payment_At = shipperData.Payment_At,
                    Salary = shipperData.Shipper.Salary,
                    ShipperPaymentId = shipperData.ShipperMonthlyPaymentID,
                    PaymentStatus = shipperData.PaymentStatus

                });
            }

            return Ok(convertViewModelShipperPayment);
        }

        [HttpGet("PaidShipperList")]
        public async Task<IActionResult> PaidShipperList()
        {
            var convertViewModelShipperPayment = new List<GetShipperPaymentViewModel>();

            var gettingPaidShipperList = await dataContext.ShipperPayments
                .Include(a => a.Shipper)
                .Where(a => a.PaymentStatus == "Shipper-Paid")
                .ToListAsync();
            foreach (var shipperData in gettingPaidShipperList)
            {
                convertViewModelShipperPayment.Add(new GetShipperPaymentViewModel
                {
                    FullName = shipperData.Shipper.FirstName + " " + shipperData.Shipper.LastName,
                    PhoneNumber = shipperData.Shipper.PhoneNumber,
                    Payment_At = shipperData.Payment_At,
                    Salary = shipperData.Shipper.Salary,
                    ShipperPaymentId = shipperData.ShipperMonthlyPaymentID,
                    PaymentStatus = shipperData.PaymentStatus
                });
            }

            return Ok(convertViewModelShipperPayment);
        }

        [HttpGet("ShipperPaymentHistory/{pageNo}")]
        public async Task<IActionResult> ShipperPaymentHistory(int pageNo)
        {
            var convertViewModelShipperPayment = new List<GetShipperPaymentViewModel>();
            var gettingPaidShipperList = await dataContext.ShipperPayments
                .Include(a => a.Shipper)
                .Where(a => a.PaymentHistory == true)
                .ToListAsync();
            foreach (var shipperPayments in gettingPaidShipperList)
            {
                convertViewModelShipperPayment.Add(new GetShipperPaymentViewModel
                {
                    ShipperID = shipperPayments.ShipperId,
                    FullName = shipperPayments.Shipper.FirstName + " " + shipperPayments.Shipper.LastName,
                    PaymentStatus = shipperPayments.PaymentStatus,
                    Payment_At = shipperPayments.Payment_At,
                    PhoneNumber = shipperPayments.Shipper.PhoneNumber,
                    Salary = shipperPayments.Shipper.Salary
                });
            }

            if (pageNo == 1)
            {

                var firstPage = convertViewModelShipperPayment.Take(12);
                return Ok(new
                {
                    shipperPaymentData = firstPage,
                    Count = convertViewModelShipperPayment.Count
                });

            }

            int skipPageSize = (pageNo - 1) * 12;
            var otherPages = convertViewModelShipperPayment.Skip(skipPageSize).Take(12);
            return Ok(new
            {
                shipperPaymentData = otherPages,
                Count = convertViewModelShipperPayment.Count
            });
        }


    }
}
