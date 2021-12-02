using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DemoAPI.Models
{
    /// <summary>
    /// Enumération permettant de limiter le choix au niveau des roles
    /// !!!Il faut que les id dans la db correspondent à nos valeurs
    /// sinon il faut effectuer un appel DB pour récupére l'id avant l'insertion!!!
    /// </summary>
    public enum RolesEnum
    {
        Admin=1,
        Membre=2
    }
    public class UserModelForm
    {
        
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } 
        [JsonIgnore]
        public string secret { get; set; }

        [Required]
        public RolesEnum IdRole { get; set; }
    }
}
