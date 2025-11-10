using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.PostFeature.Rules;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.PostRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Features.PostFeature.Commands.CreatePostCommand;

public class CreatePostCommandHandler(
    IPostWriteRepository postWriteRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IPostBusinessRules postBusinessRules) : IRequestHandler<CreatePostCommandRequest, CreatePostCommandResponse>
{
    public async Task<CreatePostCommandResponse> Handle(CreatePostCommandRequest request, CancellationToken cancellationToken)
    {
        var mappedPost = mapper.Map<Post>(request.CreatePostCommandDtoRequest);
        mappedPost.PublishedDate = DateTime.UtcNow;

        var ruleResult = await BusinessRuleEngine.RunAsync(
            () => Task.FromResult(postBusinessRules.PostTitleCannotBeEmpty(request.CreatePostCommandDtoRequest!.Title)),
            () => Task.FromResult(postBusinessRules.PostTitleLengthMustBeValid(request.CreatePostCommandDtoRequest.Title)),
            () => Task.FromResult(postBusinessRules.PostContentCannotBeEmpty(request.CreatePostCommandDtoRequest.Content)),
            () => Task.FromResult(postBusinessRules.PostContentLengthMustBeValid(request.CreatePostCommandDtoRequest.Content)),
            () => Task.FromResult(postBusinessRules.PostAuthorCannotBeEmpty(request.CreatePostCommandDtoRequest.Author)),
            () => Task.FromResult(postBusinessRules.PostAuthorLengthMustBeValid(request.CreatePostCommandDtoRequest.Author)),
            () => postBusinessRules.CategoryMustExistAsync(request.CreatePostCommandDtoRequest.CategoryId)
        );

        if (ruleResult.IsFailure)
        {
            return new CreatePostCommandResponse
            {
                Result = ruleResult
            };
        }

        await postWriteRepository.AddAsync(mappedPost, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreatePostCommandResponse
        {
            Result = Result.Success(PostOperationResultMessages.CreatedSuccess)
        };
    }
}

