using System.Collections.Generic;
using System.Threading.Tasks;
using AVB.ApiBlog.Entities.Models;

namespace AVB.ApiBlog.DataAccess.Interfaces
{
    public interface ICommentDal : IGenericRepository<Comment>
    {
        Task<List<Comment>> GetAllCommentsByArticleId(int categoryId);
    }
}