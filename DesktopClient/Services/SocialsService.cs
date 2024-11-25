using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services;
using DesktopClient.General;
using System.IO;

namespace DesktopClient.Services
{
    public class SocialsService : IDesktopSocialsService
    {
        private Server server = Server.Create();

        public void Create(SocialModel model, string contentType, Stream stream, string fileName)
        {
            server.AddSocial(model, contentType, stream, fileName);
        }

        public void Delete(int id)
        {
            server.DeleteSocial(id);
        }

        public SocialModel[] GetAll()
        {
            return server.GetAllSocials();
        }

        public void Update(SocialModel model, string contentType, Stream stream, string fileName)
        {
            server.UpdateSocial(model, contentType, stream, fileName);
        }
    }
}
