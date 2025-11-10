using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.PostFeature.DTOs;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.PostRepository;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Queries.GetAllPostQuery;

public class GetAllPostQueryHandler(IPostReadRepository _postReadRepository, IMapper _mapper) : IRequestHandler<GetAllPostQueryRequest, GetAllPostQueryResponse>
{
    public async Task<GetAllPostQueryResponse> Handle(GetAllPostQueryRequest request, CancellationToken cancellationToken)
    {
        var posts = await _postReadRepository.GetAllAsync(tracking: false, autoInclude: true);
        var mappedPosts = _mapper.Map<IEnumerable<ResultPostQueryDto>>(posts);

        if (!mappedPosts.Any())
        {
            return new GetAllPostQueryResponse
            {
                Result = ResultData<IEnumerable<ResultPostQueryDto>>.Failure(PostOperationResultMessages.GetAllNotFound, (int)HttpStatusCode.NotFound)
            };
        }
        return new GetAllPostQueryResponse
        {
            Result = ResultData<IEnumerable<ResultPostQueryDto>>.Success(mappedPosts, PostOperationResultMessages.GetAllSuccess)
        };
    }
}

