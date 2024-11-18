using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiService.Data
{
    public class Article
    {
        public int Id { get; set; }

        public string ArticleCaption { get; set; }

        public DateTime ArticlePublishDate { get; set; }

        public string ArticleText { get; set; }

        public string ArticleImageFileName { get; set; }
    }
}
