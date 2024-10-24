﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiService.Data
{
    public class Articl
    {
        public int Id { get; set; }

        public string ArticleCaption { get; set; }

        public string ArticleDescription { get; set; }

        public DateTime ArticlePublishDate { get; set; }

        public string ArticleText { get; set; }

        public string ArticleImageFileName { get; set; }
    }
}
