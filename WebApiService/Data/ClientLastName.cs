using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiService.Data
{
    public class ClientLastName
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public ICollection<Client> Clients { get; set; }
    }
}
