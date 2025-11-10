using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.DTOs;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CommentRepository;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Queries.GetByIdCommentQuery;

public class GetByIdCommentQueryHandler(ICommentReadRepository _commentReadRepository, IMapper _mapper) : IRequestHandler<GetByIdCommentQueryRequest, GetByIdCommentQueryResponse>
{
    public async Task<GetByIdCommentQueryResponse> Handle(GetByIdCommentQueryRequest request, CancellationToken cancellationToken)
    {
        var hasComment = await _commentReadRepository.GetByIdAsync(id: request.Id,
            tracking: false,
            autoInclude: true,
            cancellationToken: cancellationToken);

        if (hasComment is not null)
        {
            var mappedComment = _mapper.Map<ResultCommentQueryDto>(hasComment);
            return new GetByIdCommentQueryResponse
            {
                Result = ResultData<ResultCommentQueryDto>.Success(mappedComment, CommentOperationResultMessages.GetByIdSuccess, (int)HttpStatusCode.OK)
            };
        }

        return new GetByIdCommentQueryResponse
        {
            Result = ResultData<ResultCommentQueryDto>.Failure(CommentOperationResultMessages.GetByIdNotFound, (int)HttpStatusCode.NotFound)
        };
    }
}

