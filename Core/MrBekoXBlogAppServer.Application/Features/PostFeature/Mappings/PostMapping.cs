using AutoMapper;
using MrBekoXBlogAppServer.Application.Features.PostFeature.DTOs;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Mappings;

public class PostMapping : Profile
{
    public PostMapping()
    {
        CreateMap<Post, ResultPostQueryDto>().ReverseMap();
        CreateMap<Post, CreatePostCommandDto>().ReverseMap();
        CreateMap<Post, UpdatePostCommandDto>().ReverseMap();
    }
}

