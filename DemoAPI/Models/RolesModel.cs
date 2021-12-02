using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Models
{
    public class RolesModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nom {get;set; }
    }
}
