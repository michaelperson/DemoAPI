using BLL.Interface;
using BLL.Models;
using DemoAPI.Hubs;
using DemoAPI.Models;
using DemoAPI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<MessageHub, IHubClient> _hubCtxt;
        /// <summary>
        /// Constructeur permettant l'injection des logger, configurations,... et autres services nécessaire au bon fonctionnement du controller
        /// </summary>
        /// <param name="configuration">Objet de type <see cref="IConfiguration"/> permettant d'accèder aux infos pour le Token </param>
        /// <param name="logger">objet de type <see cref="ILogger"/> permettant le logging</param>
        /// <param name="UserBusiness">Notre service <see cref="BusinessUser"/> servant a communiquer avec la base de données</param>
        /// <param name="hubContext">Un <see cref="MessageHub"/> utilisé pour la communication SignalR</param>
        public AccountController(IConfiguration configuration, 
            ILogger<AccountController> logger, 
            IUserBusiness<BusinessUser> UserBusiness,
            IHubContext<MessageHub, IHubClient> hubContext)
        {
            _configuration = configuration;
            _UserService = UserBusiness;
            _hubCtxt = hubContext;
        }

        #region NonSecure
        /// <summary>
        /// Enregistrement d'un utilisateur 
        /// </summary>
        /// <param name="user">un <see cref="UserModelForm"/> contenant les datas nécessaires à l'enregistrement</param>
        /// <returns>un <see cref="NoContentResult"/> si l'enregistrement réussi ou un <see cref="BadRequestResult"/> dans le cas contraire</returns>
        /// <remarks>
        /// Exemple de requête:
        /// 
        ///     POST /api/Account/Register
        ///     {
        ///     "lastName": "Person",
        ///     "firstName": "Michaël",
        ///     "email": "michael.person@cognitic.be",
        ///     "password": "monpassword",
        ///     "idRole": 1 
        ///     }
        ///     !!! L'idRole doit être à 1 pour l'instant!!!
        /// 
        /// </remarks>
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
                    Password = PasswordTools.GenerateSaltedHash(Encoding.UTF8.GetBytes(user.Password), salt),
                    IdRole= (int)user.IdRole
                    
                };

                _UserService.Insert(um.ToBLL());
                return NoContent();
            }
            catch (Exception ex)
            {

                //logger l'exception
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Point de terminaisaon pour le login
        /// </summary>
        /// <param name="login">un <see cref="LoginModel"/> contenant les credentials</param>
        /// <returns>un <see cref="OkObjectResult"/> contenant le JWT Token en cas de succès et un <see cref="BadRequestObjectResult"/> dans le cas contraire</returns>
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

                try
                {
                    _hubCtxt.Clients.All.FnClientMessage("Account", $"L'utilisateur {user.LastName} s'est connecté");

                }
                catch (Exception)
                { 
                }

                string Token = TokenTool.GenerateToken(user, _configuration, new List<string> { ((RolesEnum)user.IdRole).ToString() });
  
                return new OkObjectResult(Token);
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
