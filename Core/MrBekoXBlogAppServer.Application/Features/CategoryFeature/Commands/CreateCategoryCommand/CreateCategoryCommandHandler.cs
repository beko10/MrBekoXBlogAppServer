using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.Rules;
using MrBekoXBlogAppServer.Application.Features.Constants;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CategoryRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Commands.CreateCategoryCommand;

public class CreateCategoryCommandHandler(
    ICategoryWriteRepository categoryWriteRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICategoryBusinessRules categoryBusinessRules) : IRequestHandler<CreateCategoryCommandRequest, CreateCategoryCommandResponse>
{
    public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var mappedCategory = mapper.Map<Category>(request.CreateCommandCategoryDtoRequest);

        var ruleResult = await BusinessRuleEngine.RunAsync(
            () => Task.FromResult(categoryBusinessRules.CategoryNameCannotBeEmpty(request.CreateCommandCategoryDtoRequest.CategoryName)),
            () => Task.FromResult(categoryBusinessRules.CategoryNameLengthMustBeValid(request.CreateCommandCategoryDtoRequest.CategoryName)),
            () => categoryBusinessRules.CategoryNameCannotBeDuplicatedAsync(request.CreateCommandCategoryDtoRequest.CategoryName),
            () => categoryBusinessRules.CategoryCountMustBeUnderLimitAsync()
        );


        if (ruleResult.IsFailure)
        {
            return new CreateCategoryCommandResponse
            {
                Result = ruleResult
            };
        }

        await categoryWriteRepository.AddAsync(mappedCategory, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateCategoryCommandResponse
        {
            Result = Result.Success(CategoryOperationResultMessages.CreatedSuccess)
        };
    }
}
