using BLL.Interface;
using BLL.Models;
using DemoAPI.Models;
using DemoAPI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserBusiness<BusinessUser> _UserService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IConfiguration configuration, ILogger<AccountController> logger, IUserBusiness<BusinessUser> UserBusiness)
        {
            _configuration = configuration;
            _UserService = UserBusiness;
        }

        #region NonSecure
        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public IActionResult Register(UserModelForm user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Unable to register");
                 
                //Hash password
                byte[] salt = PasswordTools.GenerateSalt();
                UserModel um = new UserModel()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Salt = salt,
                    Password = PasswordTools.GenerateSaltedHash(Encoding.UTF8.GetBytes(user.Password), salt)
                };

                _UserService.Insert(um.ToBLL());
                return NoContent();
            }
            catch (Exception ex)
            {

                //logger l'exception
                return BadRequest();
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public IActionResult Login(LoginModel login)
        {
            if (!ModelState.IsValid)
                return BadRequest("Unable to login");

            //Hash password
            UserModel user = _UserService.GetByEmail(login.Email).ToApi(); 
           
            if(PasswordTools.CheckPassword(login.Password, user.Password, user.Salt))
            {
                return new OkObjectResult(TokenTool.GenerateToken(user, _configuration, new List<string> { "Guest"}));
            }
         else
            {
                return new BadRequestObjectResult("Unable to login");
            }
         
        }
        #endregion
        #region Secure

        #endregion
    }
}
