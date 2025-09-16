using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Constants;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.DTOs;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.ContactInfoRepository;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Queries.GetByIdContactInfoQuery;

public class GetByIdContactInfoQueryHandler(
    IContactInfoReadRepository contactInfoReadRepository,
    IMapper mapper
    ) : IRequestHandler<GetByIdContactInfoQueryRequest, GetByIdContactInfoQueryResponse>
{
    public async Task<GetByIdContactInfoQueryResponse> Handle(GetByIdContactInfoQueryRequest request, CancellationToken cancellationToken)
    {
        var hasContactInfo = await contactInfoReadRepository.GetByIdAsync(request.Id!);
        if (hasContactInfo != null)
        {
            var mappedContactInfo = mapper.Map<ResultContactInfoQueryDto>(hasContactInfo);
            return new GetByIdContactInfoQueryResponse
            {
                Result = ResultData<ResultContactInfoQueryDto>.Success(
                    mappedContactInfo, 
                    ContactInfoOperationResultMessages.RetrievedSuccess,
                    (int)HttpStatusCode.OK
                )    
            };
        }
        
        return new GetByIdContactInfoQueryResponse
        {
            Result = ResultData<ResultContactInfoQueryDto>.Failure(
                ContactInfoOperationResultMessages.RetrieveFailed,
                (int)HttpStatusCode.NotFound
            )
        };  
    }
}
