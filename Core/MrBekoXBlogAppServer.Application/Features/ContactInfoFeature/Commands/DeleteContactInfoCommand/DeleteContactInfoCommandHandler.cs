using MediatR;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Rules;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.ContactInfoRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Commands.DeleteContactInfoCommand;

public class DeleteContactInfoCommandHandler(
    IContactInfoReadRepository contactInfoReadRepository,
    IContactInfoWriteRepository contactInfoWriteRepository,
    IContactInfoBusinessRules contactInfoBusinessRules,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteContactInfoCommandRequest, DeleteContactInfoCommandResponse>
{
    public async Task<DeleteContactInfoCommandResponse> Handle(DeleteContactInfoCommandRequest request, CancellationToken cancellationToken)
    {
        var deleteContactInfoBusinesRulesResult = await BusinessRuleEngine.RunAsync(
            () => contactInfoBusinessRules.CheckIfContactInfoAlreadyExistsAsync()
        );

        if (deleteContactInfoBusinesRulesResult.IsFailure)
        {
            return new DeleteContactInfoCommandResponse
            {
                Result = deleteContactInfoBusinesRulesResult
            };
        }
        var contactInfo = await contactInfoReadRepository.GetByIdAsync(request.Id);
        await contactInfoWriteRepository.RemoveAsync(contactInfo);
        await unitOfWork.SaveChangesAsync(cancellationToken:cancellationToken);
        return new DeleteContactInfoCommandResponse
        {
            Result = Result.Success(ContactInfoOperationResultMessages.DeletedSuccess)
        };


    }
}
