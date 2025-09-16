using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Constants;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Rules;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.ContactInfoRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Commands.UpdateContactInfoCommand;

public class UpdateContactInfoCommandHandler(
    IContactInfoWriteRepository contactInfoWriteRepository,
    IContactInfoReadRepository contactInfoReadRepository,
    IContactInfoBusinessRules contactInfoBusinessRules,
    IUnitOfWork unitOfWork,
    IMapper mapper
    ) : IRequestHandler<UpdateContactInfoCommandRequest, UpdateContactInfoCommandResponse>
{
    public async Task<UpdateContactInfoCommandResponse> Handle(UpdateContactInfoCommandRequest request, CancellationToken cancellationToken)
    {
        var updateContactInfoBusinessRules = await BusinessRuleEngine.RunAsync(
            () => contactInfoBusinessRules.CheckIfContactInfoAlreadyExistsAsync(),
            () => contactInfoBusinessRules.CheckIfEmailAlreadyExistsAsync(request.UpdateContactInfoDtoRequest.Email),
            () => contactInfoBusinessRules.CheckIfPhoneAlreadyExistsAsync(request.UpdateContactInfoDtoRequest.Phone)    
            );

        if(updateContactInfoBusinessRules.IsFailure)
        {
            return new UpdateContactInfoCommandResponse
            {
                Result = updateContactInfoBusinessRules
            };
        }

        var existingContactInfo = await contactInfoReadRepository.GetByIdAsync(request.UpdateContactInfoDtoRequest.Id);
        mapper.Map(request.UpdateContactInfoDtoRequest, existingContactInfo);
        await contactInfoWriteRepository.UpdateAsync(existingContactInfo);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new UpdateContactInfoCommandResponse
        {
            Result = Result.Success(ContactInfoOperationResultMessages.UpdatedSuccess)
        };
    }
}
