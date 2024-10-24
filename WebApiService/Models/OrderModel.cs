using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiService.Models
{
    public class OrderModel
    {
        [Required, MaxLength(25)]
        public string LastName { get; set; }

        [Required, MaxLength(25)]
        public string FirstName { get; set; }

        [Required, MaxLength(25)]
        public string Email { get; set; }

        [Required, MaxLength(25)]
        public string OrderText { get; set; }
    }
}
