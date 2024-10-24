using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiService.Data
{
    public class Client
    {
        public int Id { get; set; }

        public int LastNameId { get; set; }

        public int FirstNameId { get; set; }

        public int EmailId { get; set; }

        public ClientLastName LastName { get; set; }

        public ClientFirstName FirstName { get; set; }

        public ClientEmail Email { get; set; }
    }
}
