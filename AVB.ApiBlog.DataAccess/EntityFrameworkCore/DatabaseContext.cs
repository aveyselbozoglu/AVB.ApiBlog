using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AVB.ApiBlog.DataAccess.EntityFrameworkCore.Mapping;
using AVB.ApiBlog.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace AVB.ApiBlog.DataAccess.EntityFrameworkCore
{
    public class DatabaseContext : DbContext
    {


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=KJAVELINPC\\SQLEXPRESS;Database=ApiBlogDatabase;User Id=kjavelin;Password=85213589cz;Trusted_Connection=False;MultipleActiveResultSets=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new ArticleMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
        }


        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }



    }
    
}
