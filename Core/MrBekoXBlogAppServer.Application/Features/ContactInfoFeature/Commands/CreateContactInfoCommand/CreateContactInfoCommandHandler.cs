using AutoMapper;
using MediatR;
using MrBekoXBlogAppServer.Application.Common.BusinessRuleEngines;
using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Constants;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Rules;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Commands.CreateContactInfoCommand;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.ContactInfoRepository;
using MrBekoXBlogAppServer.Application.Interfaces.UnitOfWorks;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Commands.CreateContactInfoCommand;

public class CreateContactInfoCommandHandler(
    IContactInfoWriteRepository contactInfoWriteRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IContactInfoBusinessRules contactInfoBusinessRules
    ) : IRequestHandler<CreateContactInfoCommandRequest, CreateContactInfoCommandResponse>
{
    public async Task<CreateContactInfoCommandResponse> Handle(CreateContactInfoCommandRequest request, CancellationToken cancellationToken)
    {

        var contactInfoBusinessRulesResult = await BusinessRuleEngine.RunAsync(
            () => contactInfoBusinessRules.CheckIfContactInfoAlreadyExistsAsync(),
            () => contactInfoBusinessRules.CheckIfEmailAlreadyExistsAsync(request.CreateContactInfoDtoRequest!.Email!),
            () => contactInfoBusinessRules.CheckIfPhoneAlreadyExistsAsync(request.CreateContactInfoDtoRequest!.Phone!)
            );

        if (contactInfoBusinessRulesResult.IsFailure)
        {
            return new CreateContactInfoCommandResponse
            {
                Result = contactInfoBusinessRulesResult
            };
        }

        var mappedContactInfo = mapper.Map<ContactInfo>(request.CreateContactInfoDtoRequest);
        await contactInfoWriteRepository.AddAsync(mappedContactInfo, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new CreateContactInfoCommandResponse
        {
            Result = Result.Success(ContactInfoOperationResultMessages.CreatedSuccess)
        };

        throw new NotImplementedException();
    }
}
