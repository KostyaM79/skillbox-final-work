using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class OrderModel
    {
        [MaxLength(25), Required]
        public string LastName { get; set; }

        [MaxLength(25), Required]
        public string FirstName { get; set; }

        [MaxLength(50), Required]
        public string Email { get; set; }

        [MaxLength(500), Required]
        public string Message { get; set; }
    }
}
