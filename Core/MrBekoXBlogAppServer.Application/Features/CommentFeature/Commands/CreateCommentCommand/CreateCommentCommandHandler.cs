using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.CommentFeature.Rules;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.CommentRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Features.CommentFeature.Commands.CreateCommentCommand;

public class CreateCommentCommandHandler(
    ICommentWriteRepository commentWriteRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICommentBusinessRules commentBusinessRules) : IRequestHandler<CreateCommentCommandRequest, CreateCommentCommandResponse>
{
    public async Task<CreateCommentCommandResponse> Handle(CreateCommentCommandRequest request, CancellationToken cancellationToken)
    {
        var mappedComment = mapper.Map<Comment>(request.CreateCommentCommandDtoRequest);
        mappedComment.CommentDate = DateTime.UtcNow;

        var ruleResult = await BusinessRuleEngine.RunAsync(
            () => Task.FromResult(commentBusinessRules.CommentContentCannotBeEmpty(request.CreateCommentCommandDtoRequest!.Content)),
            () => Task.FromResult(commentBusinessRules.CommentContentLengthMustBeValid(request.CreateCommentCommandDtoRequest.Content)),
            () => commentBusinessRules.PostMustExistAsync(request.CreateCommentCommandDtoRequest.PostId)
        );

        if (ruleResult.IsFailure)
        {
            return new CreateCommentCommandResponse
            {
                Result = ruleResult
            };
        }

        await commentWriteRepository.AddAsync(mappedComment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateCommentCommandResponse
        {
            Result = Result.Success(CommentOperationResultMessages.CreatedSuccess)
        };
    }
}

