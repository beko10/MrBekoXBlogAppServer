using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.DTOs;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CommentRepository;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Queries.GetAllCommentQuery;

public class GetAllCommentQueryHandler(ICommentReadRepository _commentReadRepository, IMapper _mapper) : IRequestHandler<GetAllCommentQueryRequest, GetAllCommentQueryResponse>
{
    public async Task<GetAllCommentQueryResponse> Handle(GetAllCommentQueryRequest request, CancellationToken cancellationToken)
    {
        var comments = await _commentReadRepository.GetAllAsync(tracking: false, autoInclude: true);
        var mappedComments = _mapper.Map<IEnumerable<ResultCommentQueryDto>>(comments);

        if (!mappedComments.Any())
        {
            return new GetAllCommentQueryResponse
            {
                Result = ResultData<IEnumerable<ResultCommentQueryDto>>.Failure(CommentOperationResultMessages.GetAllNotFound, (int)HttpStatusCode.NotFound)
            };
        }
        return new GetAllCommentQueryResponse
        {
            Result = ResultData<IEnumerable<ResultCommentQueryDto>>.Success(mappedComments, CommentOperationResultMessages.GetAllSuccess)
        };
    }
}

