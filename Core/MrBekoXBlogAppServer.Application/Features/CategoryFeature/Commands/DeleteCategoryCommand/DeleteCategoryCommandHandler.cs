using MediatR;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.Rules;
using MrBekoXBlogAppServer.Application.Features.Constants;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CategoryRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Commands.DeleteCategoryCommand;

public class DeleteCategoryCommandHandler(
    ICategoryWriteRepository categoryWriteRepository,
    IUnitOfWork unitOfWork,
    ICategoryBusinessRules categoryBusinessRules
    ) : IRequestHandler<DeleteCategoryCommandRequest, DeleteCategoryCommandResponse>
{
    public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            categoryBusinessRules.CategoryMustExist(request.Id),
            categoryBusinessRules.CategoryCanNotBeDeletedIfHasPosts(request.Id)
        );

        if (!businessRulesResult.IsSuccess)
        {
            return new DeleteCategoryCommandResponse
            {
                Result = businessRulesResult
            };
        }

        await categoryWriteRepository.RemoveIdAsync(request.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new DeleteCategoryCommandResponse
        {
            Result = Result.Success(CategoryOperationResultMessages.DeletedSuccess)
        };
    }
}