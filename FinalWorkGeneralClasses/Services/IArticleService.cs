using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.IO;

namespace Services
{
    public interface IArticleService
    {
        ArticleModel[] GetAll();

        ArticleModel Find(int id);

        //void Create(ArticleModel model);

        void Update(ArticleModel model, string contentType, Stream stream, string fileNam);

        void Delete(int id);
    }
}
