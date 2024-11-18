using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Models
{
    public class ArticleModel
    {
        public int Id { get; set; }

        public string ArticleCaption { get; set; }

        public string ArticleDescription { get; set; }

        public DateTime ArticlePublishDate { get; set; }

        public string ArticleText { get; set; }

        public string ArticleImageFileName { get; set; }

        public void ModifyFileName(string baseAddress)
        {
            ArticleImageFileName = $"{baseAddress}api/Images/dir/articles-images/file/{ArticleImageFileName}";
        }

        public string[] Paragraphs()
        {
            Regex regex = new Regex("^.+");
            return regex.Matches(ArticleText).Select(e => e.Value).ToArray();
        }
    }
}
