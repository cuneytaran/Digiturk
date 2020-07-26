using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data.Dtos
{
    public class ArticleDto
    {
        public int ArticleId { get; set; }
        public int? UserId { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleContent { get; set; }
        public DateTime? ArticleDate { get; set; }
        public bool? Active { get; set; }
    }

    public class ArticleList
    {
        public int ArticleId { get; set; }
        public List<int> UserId { get; set; }

    }
}
