using AVB.ApiBlog.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AVB.ApiBlog.DataAccess.Interfaces
{
    public interface ICategoryDal : IGenericRepository<Category>
    {
        Task<List<Category>> GetAllOrderById();
    }
}