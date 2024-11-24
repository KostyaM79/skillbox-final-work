using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiService.Services
{
    public class SocialsService : IApiSocialsService
    {
        public void Create(string link, IFormFile file)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public SocialModel[] GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(SocialModel model, string contentType, Stream stream, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
