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
    public class CategoryRepository : GenericRepository<Category>, ICategoryDal
    {
        public async Task<List<Category>> GetAllOrderById()
        {
            await using var context = new DatabaseContext();
            return await context.Categories.OrderBy(x=> x.Id).ToListAsync();
        }
    }
}
