using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using AVB.ApiBlog.DataAccess.Concrete;
using AVB.ApiBlog.DataAccess.Interfaces;
using AVB.ApiBlog.Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AVB.ApiBlog.DataAccess
{
    public static class CustomDependencies

    {

        public static void AddDependencies(this IServiceCollection services)
        {
     
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IArticleDal, ArticleRepository>();
            services.AddScoped<ICommentDal, CommentRepository>();
            services.AddScoped<ICategoryDal, CategoryRepository>();

           
        }
    }
}
