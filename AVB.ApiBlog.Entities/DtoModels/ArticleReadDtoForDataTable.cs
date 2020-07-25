using System;
using System.Collections.Generic;
using System.Text;

namespace AVB.ApiBlog.Entities.DtoModels
{
    public class ArticleReadDtoForDataTable
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ViewCount { get; set; }

        public DateTime PublishDate { get; set; }

        public string Picture { get; set; }

        public string CategoryName { get; set; }

    }
}
