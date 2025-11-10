using MediatR;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Rules;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.PostRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;

namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Commands.DeletePostCommand;

public class DeletePostCommandHandler(
    IPostWriteRepository postWriteRepository,
    IUnitOfWork unitOfWork,
    IPostBusinessRules postBusinessRules
    ) : IRequestHandler<DeletePostCommandRequest, DeletePostCommandResponse>
{
    public async Task<DeletePostCommandResponse> Handle(DeletePostCommandRequest request, CancellationToken cancellationToken)
    {
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => postBusinessRules.PostMustExistAsync(request.Id!),
            () => postBusinessRules.PostCanNotBeDeletedIfHasCommentsAsync(request.Id!)
        );

        if (!businessRulesResult.IsSuccess)
        {
            return new DeletePostCommandResponse
            {
                Result = businessRulesResult
            };
        }

        await postWriteRepository.RemoveIdAsync(request.Id!, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new DeletePostCommandResponse
        {
            Result = Result.Success(PostOperationResultMessages.DeletedSuccess)
        };
    }
}

