using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JwtAuthenticationService.Models
{
    public class LoginModel
    {
        [Required, MaxLength(25)]
        public string Username { get; set; }

        [Required, MaxLength(25)]
        public string Password { get; set; }
    }
}
