using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Constants;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.DTOs;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.ContactInfoRepository;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Queries.GetAllContactInfoQuery;

public class GetAllContactInfoQueryHandler(
    IContactInfoReadRepository contactInfoReadRepository,
    IMapper mapper
    ) : IRequestHandler<GetAllContactInfoQueryRequest, GetAllContactInfoQueryResponse>
{

    public async Task<GetAllContactInfoQueryResponse> Handle(GetAllContactInfoQueryRequest request, CancellationToken cancellationToken)
    {
        var contactInfos = await contactInfoReadRepository.GetAllAsync();
        if(contactInfos is not null && contactInfos.Any())
        {
            var resultDtos = mapper.Map<IEnumerable<ResultContactInfoQueryDto>>(contactInfos);
            return new GetAllContactInfoQueryResponse
            {
                Result = ResultData<IEnumerable<ResultContactInfoQueryDto>>.Success(resultDtos,
                    ContactInfoOperationResultMessages.ListRetrievedSuccess,
                    (int)HttpStatusCode.OK
                )
            };
        }
        return new GetAllContactInfoQueryResponse
        {
            Result = ResultData<IEnumerable<ResultContactInfoQueryDto>>.Failure( 
                ContactInfoOperationResultMessages.NoContactInfoFound, 
                (int)HttpStatusCode.NotFound
            )
        };
       
    }
}
