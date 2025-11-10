using MediatR;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Rules;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CommentRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Commands.DeleteCommentCommand;

public class DeleteCommentCommandHandler(
    ICommentWriteRepository commentWriteRepository,
    IUnitOfWork unitOfWork,
    ICommentBusinessRules commentBusinessRules
    ) : IRequestHandler<DeleteCommentCommandRequest, DeleteCommentCommandResponse>
{
    public async Task<DeleteCommentCommandResponse> Handle(DeleteCommentCommandRequest request, CancellationToken cancellationToken)
    {
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => commentBusinessRules.CommentMustExistAsync(request.Id!),
            () => commentBusinessRules.CommentCanNotBeDeletedIfHasSubCommentsAsync(request.Id!)
        );

        if (!businessRulesResult.IsSuccess)
        {
            return new DeleteCommentCommandResponse
            {
                Result = businessRulesResult
            };
        }

        await commentWriteRepository.RemoveIdAsync(request.Id!, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new DeleteCommentCommandResponse
        {
            Result = Result.Success(CommentOperationResultMessages.DeletedSuccess)
        };
    }
}

