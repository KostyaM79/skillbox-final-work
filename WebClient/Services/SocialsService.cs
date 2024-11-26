using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using WebClient.Data;
using Microsoft.Extensions.Configuration;

namespace WebClient.Services
{
    /// <summary>
    /// Управляет ссылками на соц. сети
    /// </summary>
    public class SocialsService : IClientSocialsService
    {
        private IConfiguration config;
        private IDatabase database;

        public SocialsService(IDatabase database, IConfiguration configuration)
        {
            this.database = database;
            config = configuration;
        }

        public SocialModel[] GetAll()
        {
            SocialModel[] socials = database.GetAllSocials();
            foreach (SocialModel social in socials)
                social.FileName = $"{config["ApiLocation"]}{social.FileName}";
            return socials;
        }

        public void Update(string[] links, IFormFileCollection files, string token)
        {
            database.UpdateSocials(links, files, token);
        }
    }
}
