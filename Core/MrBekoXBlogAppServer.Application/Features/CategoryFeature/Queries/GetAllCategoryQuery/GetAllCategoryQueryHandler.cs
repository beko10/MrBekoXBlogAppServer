using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.DTOs;
using MrBekoXBlogAppServer.Application.Features.Constants;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CategoryRepository;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Queries.GetAllCategoryQuery;

public class GetAllCategoryQueryHandler(ICategoryReadRepository _categoryReadRepository,IMapper _mapper) : IRequestHandler<GetAllCategoryQueryRequest, GetAllCategoryQueryResponse>
{
    public async Task<GetAllCategoryQueryResponse> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
    {
        var categories = await _categoryReadRepository.GetAllAsync();
        var mappedCategories = _mapper.Map<IEnumerable<ResultCategoryQueryDto>>(categories);

        if (!mappedCategories.Any())
        {
            return new GetAllCategoryQueryResponse
            {
                Result = ResultData<IEnumerable<ResultCategoryQueryDto>>.Failure(CategoryOperationResultMessages.GetAllNotFound,(int)HttpStatusCode.NotFound)
            };
        }
        return new GetAllCategoryQueryResponse
        {
            Result = ResultData<IEnumerable<ResultCategoryQueryDto>>.Success(mappedCategories,CategoryOperationResultMessages.GetAllSuccess)
        };
    }
}
