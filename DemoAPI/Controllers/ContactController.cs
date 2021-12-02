using BLL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Tools;
using BLL.Models;
using DemoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DemoAPI.Tools.Logging.Interfaces;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContactController : ControllerBase
    {
        //private List<string> maListe = new List<string>();
        private IContactBusiness<BusinessContact> _contactService;
        private readonly ILoggerManager _logger;
        public ContactController(IContactBusiness<BusinessContact> contactBusiness, ILoggerManager logger)
        {
            _contactService = contactBusiness;
            _logger = logger;
            //maListe.Add("test");
            //maListe.Add("coucou");
            //maListe.Add("pizza");
            //maListe.Add("couscous");
        }
        [HttpGet]
         
         [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            return Ok(_contactService.GetAll().Select(c => c.ToApi()));
            //return Ok(maListe);
                     
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Membre")]
        public IActionResult GetById(int id)
        {
#if DEBUG
            _logger.LogDebug($"[GET api/Contact/{id}] Demande des informations du contact ayant l'id {id}");
#endif

            return Ok(_contactService.GetById(id).ToApi());
        }

        [HttpDelete("{id}")] 
        public IActionResult Delete(int id)
        {
            try
            {

                _contactService.Delete(id);
                return Ok("Tout s'est bien passé");
            }
            catch (Exception e)
            {
                _logger.LogCritic($"[DELETE api/Contact/{id}] Erreur lors de la suppression. \n\r Exception : {e.Message}");
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        public IActionResult Insert(ContactForm c)
        {
            if (!ModelState.IsValid)
                return BadRequest("Ca a foiré");
            _contactService.Insert(c.ToBLL());
            return Ok("Enregistrement effectué");
        }

        [HttpPut]
        [Authorize(Roles = "Membre") ]
        public IActionResult Update(ContactForm c)
        {
            if (!ModelState.IsValid)
                return BadRequest("Ca a foiré");
            if(c.Id == 0)
            {
                return BadRequest("L'id doit être renseigné");
            }
            _contactService.Update(c.ToBLL());
            return Ok("Modification effectuée");
        }
    }
}
