using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Rules;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.PostRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;

namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Commands.UpdatePostCommand;

public class UpdatePostCommandHandler(
    IPostWriteRepository postWriteRepository,
    IPostReadRepository postReadRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IPostBusinessRules postBusinessRules) : IRequestHandler<UpdatePostCommandRequest, UpdatePostCommandResponse>
{
    public async Task<UpdatePostCommandResponse> Handle(UpdatePostCommandRequest request, CancellationToken cancellationToken)
    {
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => postBusinessRules.PostMustExistAsync(request.UpdatePostCommandDtoRequest!.Id),
            () => Task.FromResult(postBusinessRules.PostTitleCannotBeEmpty(request.UpdatePostCommandDtoRequest.Title)),
            () => Task.FromResult(postBusinessRules.PostTitleLengthMustBeValid(request.UpdatePostCommandDtoRequest.Title)),
            () => Task.FromResult(postBusinessRules.PostContentCannotBeEmpty(request.UpdatePostCommandDtoRequest.Content)),
            () => Task.FromResult(postBusinessRules.PostContentLengthMustBeValid(request.UpdatePostCommandDtoRequest.Content)),
            () => Task.FromResult(postBusinessRules.PostAuthorCannotBeEmpty(request.UpdatePostCommandDtoRequest.Author)),
            () => Task.FromResult(postBusinessRules.PostAuthorLengthMustBeValid(request.UpdatePostCommandDtoRequest.Author)),
            () => postBusinessRules.CategoryMustExistAsync(request.UpdatePostCommandDtoRequest.CategoryId)
        );

        if (businessRulesResult.IsFailure)
        {
            return new UpdatePostCommandResponse
            {
                Result = businessRulesResult
            };
        }

        var existingPost = await postReadRepository.GetByIdAsync(request.UpdatePostCommandDtoRequest.Id);
        mapper.Map(request.UpdatePostCommandDtoRequest, existingPost);
        await postWriteRepository.UpdateAsync(existingPost);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdatePostCommandResponse
        {
            Result = Result.Success(PostOperationResultMessages.UpdatedSuccess)
        };
    }
}

