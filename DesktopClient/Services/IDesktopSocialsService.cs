using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Services;
using Models;

namespace DesktopClient.Services
{
    public interface IDesktopSocialsService : ISocialsService
    {
        void Create(SocialModel model, string contentType, Stream stream, string fileName);

    }
}
