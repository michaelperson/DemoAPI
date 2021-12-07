using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DemoAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [JsonIgnore]
        public byte[] Password { get; set; }
        [JsonIgnore]
        public byte[] Salt { get; set; }

        [Required]
        public int IdRole { get; set; }

        [JsonIgnore]
        public string SignalRConnectionId { get; set; }
    }
}
