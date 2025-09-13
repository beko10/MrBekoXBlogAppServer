using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.DTOs;
using MrBekoXBlogAppServer.Application.Features.Constants.GetByIdCategoryQueryConstants;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CategoryRepository;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Queries.GetByIdCategoryQuery;

public class GetByIdCategoryQueryHandler(ICategoryReadRepository _categoryReadRepository,IMapper _mapper) : IRequestHandler<GetByIdCategoryQueryRequest, GetByIdCategoryQueryResponse>
{
    public async Task<GetByIdCategoryQueryResponse> Handle(GetByIdCategoryQueryRequest request, CancellationToken cancellationToken)
    {
        //fluent validation pipeline behavior
        //todo : Business Rules 
        var hasCategory = await _categoryReadRepository.GetByIdAsync(id : request.Id, cancellationToken : cancellationToken);

        if(hasCategory is not null)
        {
            var mappedCategory= _mapper.Map<ResultCategoryQueryDto>(hasCategory);
            return new GetByIdCategoryQueryResponse
            {
                Result = ResultData<ResultCategoryQueryDto>.Success(mappedCategory,CategoryOperationResultMessages.GetByIdSuccess, (int)HttpStatusCode.OK)
            };
        }

        return new GetByIdCategoryQueryResponse 
        { 
            Result = ResultData<ResultCategoryQueryDto>.Failure(CategoryOperationResultMessages.GetByIdNotFound, (int)HttpStatusCode.NotFound) 
        };
    }
}
