using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.PostFeature.DTOs;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.PostRepository;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Queries.GetByIdPostQuery;

public class GetByIdPostQueryHandler(IPostReadRepository _postReadRepository, IMapper _mapper) : IRequestHandler<GetByIdPostQueryRequest, GetByIdPostQueryResponse>
{
    public async Task<GetByIdPostQueryResponse> Handle(GetByIdPostQueryRequest request, CancellationToken cancellationToken)
    {
        var hasPost = await _postReadRepository.GetByIdAsync(id: request.Id,
            tracking: false,
            autoInclude: true,
            cancellationToken: cancellationToken);

        if (hasPost is not null)
        {
            var mappedPost = _mapper.Map<ResultPostQueryDto>(hasPost);
            return new GetByIdPostQueryResponse
            {
                Result = ResultData<ResultPostQueryDto>.Success(mappedPost, PostOperationResultMessages.GetByIdSuccess, (int)HttpStatusCode.OK)
            };
        }

        return new GetByIdPostQueryResponse
        {
            Result = ResultData<ResultPostQueryDto>.Failure(PostOperationResultMessages.GetByIdNotFound, (int)HttpStatusCode.NotFound)
        };
    }
}

