using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Models;

namespace Services
{
    public interface ISocialsService
    {
        SocialModel[] GetAll();

        void Update(SocialModel model, string contentType, Stream stream, string fileName);

        void Delete(int id);
    }
}
