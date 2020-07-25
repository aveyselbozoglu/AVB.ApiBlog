using AVB.ApiBlog.Entities.Models;
using System.Collections.Generic;

namespace AVB.ApiBlog.Entities.DtoModels
{
    public class CategoryReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Article> Articles { get; set; }

        public CategoryReadDto()
        {
            Articles = new List<Article>();
        }
    }
}