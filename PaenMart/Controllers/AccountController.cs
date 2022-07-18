using Business_Core.Entities.Identity;
using Business_Core.Entities.Identity.AdminAccount;
using Bussiness_Core.IServices;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Presentation.AppSettingClasses;
using Presentation.ViewModel.IdentityViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<CustomIdentity> userManager;
        private readonly SignInManager<CustomIdentity> signInManger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationSettings _appSettings;
        private readonly IUserPhotoService _UserPhotoService;
        private readonly DataContext _dataContext;



        public AccountController(
            UserManager<CustomIdentity> myuserManager,
            SignInManager<CustomIdentity> mysignIngManage,
            IOptions<ApplicationSettings> appSettings,
            RoleManager<IdentityRole> roleManager,
            IUserPhotoService userPhotoService,
            DataContext dataContext)
        {
            this.userManager = myuserManager;
            this.signInManger = mysignIngManage;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
            _UserPhotoService = userPhotoService;
            _dataContext = dataContext;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel model)
        {
            var user = new CustomIdentity();
            var findingUserEntryCount = userManager.Users.Count();
            string[] addDefaultRoles = { "Admin", "User", "Employee", "Shipper" };
            if (findingUserEntryCount == 0)
            {
                foreach (var singleRole in addDefaultRoles)
                {
                var identityRole =  new IdentityRole { Name = singleRole };
                await _roleManager.CreateAsync(identityRole);

                }
            }

            var findingUser = await userManager.FindByNameAsync(model.FullName.Replace(" ", ""));
            if (findingUser == null)
            {
                user.FullName = model.FullName;
                user.UserName = model.FullName.Replace(" ", "");
            }
            else
            {
                return BadRequest($"Username already taken {model.FullName}");

            }
            var findingEmail = await userManager.FindByEmailAsync(model.Email);
            if (findingEmail == null)
            {
                user.Email = model.Email;
            }
            else
            {
                return BadRequest($"Email already taken {model.Email}");
            }
            var dataInserting = await userManager.CreateAsync(user, model.Password);
            IdentityResult result = null;
            if (dataInserting.Succeeded)
            {
                await signInManger.SignInAsync(user, isPersistent: false);

                if(findingUserEntryCount == 0)
                {
                    if (!await userManager.IsInRoleAsync(user, "Admin"))
                    {
                        result = await userManager.AddToRoleAsync(user, "Admin");
                    }

                    // entry in account balance table as well 

                    AdminAccount adminAccount = new AdminAccount
                    {
                        BalanceSituation = "Add",
                        BeforeBalance = 0,
                        CurrentBalance = 0,
                        TransactionPurpose = "Initial State",
                        UserId = user.Id,
                        TransactionDateTime = DateTime.Now
                    };

                    await _dataContext.AdminAccounts.AddAsync(adminAccount);
                    await _dataContext.SaveChangesAsync();

                }
                else
                {
                    if (!await userManager.IsInRoleAsync(user, "User"))
                    {
                        result = await userManager.AddToRoleAsync(user, "User");
                    }
                }

               

                return Created($"{Request.Scheme://request.host}{Request.Path}/{model.Id}", model);
            }
            return BadRequest(dataInserting.Errors);
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LogInViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var findUrlWhoseIsMainTrueUrl = await _UserPhotoService.GetMainPhotoForUser(user.Id);
                var roles = await userManager.GetRolesAsync(user);
                //HERE I USE THE CONDITION BECUASE WHENEVER USER HAVE NOT HAVING A ROLE IN A DATABASE THEN LOG HIM WITHOUT THE ROLE BUT HE/SHE NEED TO AUTHENTICATED OR REGISTAR FIRST
                if (roles.Count >= 1)
                {
                    string userRoles = String.Join(", ", roles); // if multiple roles are user having then these role will be saved in token

                    IdentityOptions identityOptions = new IdentityOptions();

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                    {
                        // here we are passing the data or store the data in the token and then encoded it which is userId and user role

                    new Claim("UserID", user.Id.ToString()),
                    new Claim(identityOptions.ClaimsIdentity.RoleClaimType ,userRoles),


                    }),
                        // donot by default expire it the token until user doesn't log out and when user log-out it then remove from localstorage
                        Expires = DateTime.UtcNow.AddDays(7), //the token will expire after the one day here
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);

                    if (findUrlWhoseIsMainTrueUrl == null)
                    {
                        return Ok(new { token = token, photoUrl = "" }); // here we only pass token and it is a valid way and good practice to do and when token is passed then send the request to getUserProfile data

                    }

                    //  return Ok(new { token,  user.FullName, user.Email, user.Id, findUrlWhoseIsMainTrue?.URL });
                    return Ok(new {token = token, photoUrl = findUrlWhoseIsMainTrueUrl.URL }); // here we only pass token and it is a valid way and good practice to do and when token is passed then send the request to getUserProfile data
                }
                else
                {
                    return BadRequest(new { message = "Username or password is incorrect. please try again" });
                }


            }
            return BadRequest("Something going fishy!");
        }

        [HttpPost("GetUserProfile")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile(GetTokenViewMode getTokenViewMode)
        {
            // when logged in action is perform and token is generated and send back to client side then again from there send request again to server to this method to get the data
            // here we will get the userId from token to decode from it and then search it in the database and get that data
            // string userId = User.Claims.First(c => c.Type == "UserID").Value;
            CustomIdentity user = new CustomIdentity();

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(getTokenViewMode.Token);
            var tokenS = jsonToken as JwtSecurityToken;
            var gettingDataFromTokenObj = tokenS?.Payload;
            foreach (var singleProp in gettingDataFromTokenObj)
            {
                if(singleProp.Key == "UserID")
                {
                    user = await userManager.FindByIdAsync(singleProp.Value.ToString());
                    break;
                }
            }
            //   var findUrlWhoseIsMainTrue = await _UserPhotoService.GetMainPhotoForUser(user.Id); // this is best place to get the photo or image
            return Ok( new
            {
                user.Id,
                user.FullName,
                user.Email,
              //  findUrlWhoseIsMainTrue?.URL
            });

        }
    }
}
