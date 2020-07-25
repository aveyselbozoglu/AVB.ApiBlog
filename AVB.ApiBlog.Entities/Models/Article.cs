using System;
using System.Collections.Generic;

namespace AVB.ApiBlog.Entities.Models
{
    public class Article
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ContentSummary { get; set; }

        public string ContentMain { get; set; }

        public DateTime PublishDate { get; set; }

        public string Picture { get; set; }

        public int ViewCount { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Comment> Comments { get; set; }
    }
}