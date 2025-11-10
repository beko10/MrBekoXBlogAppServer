using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Rules;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CommentRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Commands.UpdateCommentCommand;

public class UpdateCommentCommandHandler(
    ICommentWriteRepository commentWriteRepository,
    ICommentReadRepository commentReadRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICommentBusinessRules commentBusinessRules) : IRequestHandler<UpdateCommentCommandRequest, UpdateCommentCommandResponse>
{
    public async Task<UpdateCommentCommandResponse> Handle(UpdateCommentCommandRequest request, CancellationToken cancellationToken)
    {
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => commentBusinessRules.CommentMustExistAsync(request.UpdateCommentCommandDtoRequest!.Id),
            () => Task.FromResult(commentBusinessRules.CommentContentCannotBeEmpty(request.UpdateCommentCommandDtoRequest.Content)),
            () => Task.FromResult(commentBusinessRules.CommentContentLengthMustBeValid(request.UpdateCommentCommandDtoRequest.Content))
        );

        if (businessRulesResult.IsFailure)
        {
            return new UpdateCommentCommandResponse
            {
                Result = businessRulesResult
            };
        }

        var existingComment = await commentReadRepository.GetByIdAsync(request.UpdateCommentCommandDtoRequest.Id);
        mapper.Map(request.UpdateCommentCommandDtoRequest, existingComment);
        await commentWriteRepository.UpdateAsync(existingComment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateCommentCommandResponse
        {
            Result = Result.Success(CommentOperationResultMessages.UpdatedSuccess)
        };
    }
}

