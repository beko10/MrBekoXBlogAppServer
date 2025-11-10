using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.DTOs;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.SubCommentRepository;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Queries.GetAllSubCommentQuery;

public class GetAllSubCommentQueryHandler(ISubCommentReadRepository _subCommentReadRepository, IMapper _mapper) : IRequestHandler<GetAllSubCommentQueryRequest, GetAllSubCommentQueryResponse>
{
    public async Task<GetAllSubCommentQueryResponse> Handle(GetAllSubCommentQueryRequest request, CancellationToken cancellationToken)
    {
        var subComments = await _subCommentReadRepository.GetAllAsync(tracking: false, autoInclude: true);
        var mappedSubComments = _mapper.Map<IEnumerable<ResultSubCommentQueryDto>>(subComments);

        if (!mappedSubComments.Any())
        {
            return new GetAllSubCommentQueryResponse
            {
                Result = ResultData<IEnumerable<ResultSubCommentQueryDto>>.Failure(SubCommentOperationResultMessages.GetAllNotFound, (int)HttpStatusCode.NotFound)
            };
        }
        return new GetAllSubCommentQueryResponse
        {
            Result = ResultData<IEnumerable<ResultSubCommentQueryDto>>.Success(mappedSubComments, SubCommentOperationResultMessages.GetAllSuccess)
        };
    }
}

