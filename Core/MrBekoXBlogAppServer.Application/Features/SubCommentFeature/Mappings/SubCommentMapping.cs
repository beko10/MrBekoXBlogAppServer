using AutoMapper;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.DTOs;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Mappings;

public class SubCommentMapping : Profile
{
    public SubCommentMapping()
    {
        CreateMap<SubComment, ResultSubCommentQueryDto>().ReverseMap();
        CreateMap<SubComment, CreateSubCommentCommandDto>().ReverseMap();
        CreateMap<SubComment, UpdateSubCommentCommandDto>().ReverseMap();
    }
}

