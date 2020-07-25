using AutoMapper;
using AVB.ApiBlog.Entities.DtoModels;
using AVB.ApiBlog.Entities.Models;

namespace AVB.ApiBlog.DataAccess.AutoMapper
{
    public class AutoMaps : Profile
    {
        public AutoMaps()
        {
            CreateMap<Category, CategoryReadDto>();

            CreateMap<Article, ArticleReadDto>().ForMember(x => x.CategoryReadDto, m2 => m2.MapFrom(x => x.Category));

            CreateMap<ArticlePutDto, Article>()
            .ForMember(dest => dest.ContentSummary, opt => opt.MapFrom(src => src.ContentSummary))
            .ForMember(dest => dest.Title, opt => opt.MapFrom((src => src.Title)))
            .ForMember(dest => dest.ContentMain, opt => opt.MapFrom(src => src.ContentMain))
            .ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.Picture))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
        }
    }
}