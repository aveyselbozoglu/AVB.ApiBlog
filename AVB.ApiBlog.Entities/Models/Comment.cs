using System;

namespace AVB.ApiBlog.Entities.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        public DateTime PublishDate { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}