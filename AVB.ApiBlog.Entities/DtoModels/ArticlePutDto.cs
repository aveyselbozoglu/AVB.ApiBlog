using System;
using System.Collections.Generic;
using System.Text;
using AVB.ApiBlog.Entities.Models;

namespace AVB.ApiBlog.Entities.DtoModels
{
    public class ArticlePutDto
    {
        public string Title { get; set; }

        public string ContentSummary { get; set; }

        public string ContentMain { get; set; }

        public string Picture { get; set; }

        public int CategoryId { get; set; }

    }
}
