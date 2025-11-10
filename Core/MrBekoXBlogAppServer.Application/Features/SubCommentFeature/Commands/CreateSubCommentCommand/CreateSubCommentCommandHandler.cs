using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Rules;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.SubCommentRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Commands.CreateSubCommentCommand;

public class CreateSubCommentCommandHandler(
    ISubCommentWriteRepository subCommentWriteRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ISubCommentBusinessRules subCommentBusinessRules) : IRequestHandler<CreateSubCommentCommandRequest, CreateSubCommentCommandResponse>
{
    public async Task<CreateSubCommentCommandResponse> Handle(CreateSubCommentCommandRequest request, CancellationToken cancellationToken)
    {
        var mappedSubComment = mapper.Map<SubComment>(request.CreateSubCommentCommandDtoRequest);
        mappedSubComment.CommentDate = DateTime.UtcNow;

        var ruleResult = await BusinessRuleEngine.RunAsync(
            () => Task.FromResult(subCommentBusinessRules.SubCommentContentCannotBeEmpty(request.CreateSubCommentCommandDtoRequest!.Content)),
            () => Task.FromResult(subCommentBusinessRules.SubCommentContentLengthMustBeValid(request.CreateSubCommentCommandDtoRequest.Content)),
            () => subCommentBusinessRules.CommentMustExistAsync(request.CreateSubCommentCommandDtoRequest.CommentId)
        );

        if (ruleResult.IsFailure)
        {
            return new CreateSubCommentCommandResponse
            {
                Result = ruleResult
            };
        }

        await subCommentWriteRepository.AddAsync(mappedSubComment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateSubCommentCommandResponse
        {
            Result = Result.Success(SubCommentOperationResultMessages.CreatedSuccess)
        };
    }
}

