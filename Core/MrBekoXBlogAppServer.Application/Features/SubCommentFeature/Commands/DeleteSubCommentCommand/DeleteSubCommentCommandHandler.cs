using MediatR;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Rules;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.SubCommentRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Commands.DeleteSubCommentCommand;

public class DeleteSubCommentCommandHandler(
    ISubCommentWriteRepository subCommentWriteRepository,
    IUnitOfWork unitOfWork,
    ISubCommentBusinessRules subCommentBusinessRules
    ) : IRequestHandler<DeleteSubCommentCommandRequest, DeleteSubCommentCommandResponse>
{
    public async Task<DeleteSubCommentCommandResponse> Handle(DeleteSubCommentCommandRequest request, CancellationToken cancellationToken)
    {
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => subCommentBusinessRules.SubCommentMustExistAsync(request.Id!)
        );

        if (!businessRulesResult.IsSuccess)
        {
            return new DeleteSubCommentCommandResponse
            {
                Result = businessRulesResult
            };
        }

        await subCommentWriteRepository.RemoveIdAsync(request.Id!, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new DeleteSubCommentCommandResponse
        {
            Result = Result.Success(SubCommentOperationResultMessages.DeletedSuccess)
        };
    }
}

