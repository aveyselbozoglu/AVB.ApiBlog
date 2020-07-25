using AVB.ApiBlog.Entities.Models;
using System;
using System.Collections.Generic;

namespace AVB.ApiBlog.Entities.DtoModels
{
    public class ArticleReadDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ContentSummary { get; set; }

        public string ContentMain { get; set; }

        public DateTime PublishDate { get; set; }

        public string Picture { get; set; }

        public int ViewCount { get; set; }

        public int CategoryId { get; set; }

        //değişicek categoryreaddto
        public CategoryReadDto CategoryReadDto { get; set; }

        public List<Comment> Comments { get; set; }

        public int CommentCount { get; set; }
    }
}