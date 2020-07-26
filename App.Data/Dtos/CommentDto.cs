using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data.Dtos
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public int? ArticleId { get; set; }
        public int? UserId { get; set; }
        public string Comment { get; set; }
        public DateTime? CommentDate { get; set; }
        public bool? Active { get; set; }
    }

    public class CommentList
    {
        public int CommentId { get; set; }
        //public int? ArticleId { get; set; }
        public List<int> ArticleId { get; set; }
        public List<int> UserId { get; set; }

    }
}
