using AutoMapper;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.DTOs;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Mappings;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<Category,ResultCategoryQueryDto>().ReverseMap();
        CreateMap<Category,CreateCommandCategoryDto > ().ReverseMap();
        CreateMap<Category,UpdateCommandCategoryDto>().ReverseMap();
    }
}
