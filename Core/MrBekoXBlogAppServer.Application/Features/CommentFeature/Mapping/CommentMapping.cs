using AutoMapper;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.DTOs;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Mapping;

public class CommentMapping : Profile
{
    public CommentMapping()
    {
        CreateMap<Comment, ResultCommentQueryDto>().ReverseMap();
        CreateMap<Comment, CreateCommentCommandDto>().ReverseMap();
        CreateMap<Comment, UpdateCommentCommandDto>().ReverseMap();
    }
}
