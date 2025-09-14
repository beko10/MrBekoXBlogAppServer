using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.Rules;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CategoryRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Commands.UpdateCategoryCommand;

public class UpdateCategoryCommandHandler(
    ICategoryWriteRepository categoryWriteRepository,
    ICategoryReadRepository categoryReadRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICategoryBusinessRules categoryBusinessRules) : IRequestHandler<UpdateCategoryCommandRequest, UpdateCategoryCommandResponse>
{
    public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
    {

        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => categoryBusinessRules.CategoryMustExistAsync(request.UpdateCommandCategoryDtoRequest.Id),
            () => Task.FromResult(categoryBusinessRules.CategoryNameCannotBeEmpty(request.UpdateCommandCategoryDtoRequest.CategoryName)),
            () => Task.FromResult(categoryBusinessRules.CategoryNameLengthMustBeValid(request.UpdateCommandCategoryDtoRequest.CategoryName)),
            () => categoryBusinessRules.CategoryNameCannotBeDuplicatedAsync(
             request.UpdateCommandCategoryDtoRequest.CategoryName,
             request.UpdateCommandCategoryDtoRequest.Id)
        );


        if (businessRulesResult.IsFailure)
        {
            return new UpdateCategoryCommandResponse
            {
                Result = businessRulesResult
            };
        }

        
        var existingCategory = await categoryReadRepository.GetByIdAsync(request.UpdateCommandCategoryDtoRequest.Id);
        mapper.Map(request.UpdateCommandCategoryDtoRequest, existingCategory);
        await categoryWriteRepository.UpdateAsync(existingCategory);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateCategoryCommandResponse
        {
            Result = Result.Success(CategoryOperationResultMessages.UpdatedSuccess)
        };
    }
}
