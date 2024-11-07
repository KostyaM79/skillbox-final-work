using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.Data;

namespace WebApiService.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext context;

        public ClientService(AppDbContext context)
        {
            this.context = context;
        }


        public bool Create()
        {
            throw new NotImplementedException();
        }
    }
}
