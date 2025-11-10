using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Rules;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.SubCommentRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;

namespace MrBekoXBlogAppServer.Application.Features.SubCommentFeature.Commands.UpdateSubCommentCommand;

public class UpdateSubCommentCommandHandler(
    ISubCommentWriteRepository subCommentWriteRepository,
    ISubCommentReadRepository subCommentReadRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ISubCommentBusinessRules subCommentBusinessRules) : IRequestHandler<UpdateSubCommentCommandRequest, UpdateSubCommentCommandResponse>
{
    public async Task<UpdateSubCommentCommandResponse> Handle(UpdateSubCommentCommandRequest request, CancellationToken cancellationToken)
    {
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => subCommentBusinessRules.SubCommentMustExistAsync(request.UpdateSubCommentCommandDtoRequest!.Id),
            () => Task.FromResult(subCommentBusinessRules.SubCommentContentCannotBeEmpty(request.UpdateSubCommentCommandDtoRequest.Content)),
            () => Task.FromResult(subCommentBusinessRules.SubCommentContentLengthMustBeValid(request.UpdateSubCommentCommandDtoRequest.Content))
        );

        if (businessRulesResult.IsFailure)
        {
            return new UpdateSubCommentCommandResponse
            {
                Result = businessRulesResult
            };
        }

        var existingSubComment = await subCommentReadRepository.GetByIdAsync(request.UpdateSubCommentCommandDtoRequest.Id);
        mapper.Map(request.UpdateSubCommentCommandDtoRequest, existingSubComment);
        await subCommentWriteRepository.UpdateAsync(existingSubComment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateSubCommentCommandResponse
        {
            Result = Result.Success(SubCommentOperationResultMessages.UpdatedSuccess)
        };
    }
}

