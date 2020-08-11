using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVB.ApiBlog.DataAccess.EntityFrameworkCore;
using AVB.ApiBlog.DataAccess.Interfaces;
using AVB.ApiBlog.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace AVB.ApiBlog.DataAccess.Concrete
{
    public class CommentRepository: GenericRepository<Comment>, ICommentDal
    {
        public async Task<List<Comment>> GetAllCommentsByArticleId(int id)
        {
            await using var context = new DatabaseContext();
            return await context.Comments.Where(x => x.ArticleId == id).ToListAsync();
        }



    }
}
