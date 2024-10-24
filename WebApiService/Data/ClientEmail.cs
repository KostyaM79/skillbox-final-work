using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiService.Data
{
    public class ClientEmail
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public ICollection<Client> Clients { get; set; }
    }
}
