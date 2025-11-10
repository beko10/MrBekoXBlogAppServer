using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.DTOs;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.SubCommentRepository;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Queries.GetByIdSubCommentQuery;

public class GetByIdSubCommentQueryHandler(ISubCommentReadRepository _subCommentReadRepository, IMapper _mapper) : IRequestHandler<GetByIdSubCommentQueryRequest, GetByIdSubCommentQueryResponse>
{
    public async Task<GetByIdSubCommentQueryResponse> Handle(GetByIdSubCommentQueryRequest request, CancellationToken cancellationToken)
    {
        var hasSubComment = await _subCommentReadRepository.GetByIdAsync(id: request.Id,
            tracking: false,
            autoInclude: true,
            cancellationToken: cancellationToken);

        if (hasSubComment is not null)
        {
            var mappedSubComment = _mapper.Map<ResultSubCommentQueryDto>(hasSubComment);
            return new GetByIdSubCommentQueryResponse
            {
                Result = ResultData<ResultSubCommentQueryDto>.Success(mappedSubComment, SubCommentOperationResultMessages.GetByIdSuccess, (int)HttpStatusCode.OK)
            };
        }

        return new GetByIdSubCommentQueryResponse
        {
            Result = ResultData<ResultSubCommentQueryDto>.Failure(SubCommentOperationResultMessages.GetByIdNotFound, (int)HttpStatusCode.NotFound)
        };
    }
}

