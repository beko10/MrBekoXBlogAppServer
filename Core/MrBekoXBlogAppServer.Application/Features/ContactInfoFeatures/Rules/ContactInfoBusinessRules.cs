using MrBekoXBlogAppServer.Application.Common.Results;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Constants;
using MrBekoXBlogAppServer.Application.Interfaces.Repositories.ContactInfoRepository;
using System.Net;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Rules;

public class ContactInfoBusinessRules(IContactInfoReadRepository contactInfoReadRepository) : IContactInfoBusinessRules
{
    public async Task<Result> CheckIfContactInfoAlreadyExistsAsync()
    {
        var existingContactInfos = await contactInfoReadRepository.GetAllAsync();
        if (existingContactInfos.Any())
        {
            var exception = new BusinessRuleException(
                message: ContactInfoBusinessRuleErrorMessages.ContactInfoAlreadyExists
            );
            return Result.Failure(
                error: exception,
                message: ContactInfoBusinessRuleMessages.contactInfoBusinessRulesOperationCompletedNotSuccessfully,
                statusCode: (int)HttpStatusCode.Conflict
            );
        }
        return Result.Success();
    }

    public async Task<Result> CheckIfContactInfoExistsByIdAsync(string id)
    {
        var existingContactInfo = await contactInfoReadRepository.GetByIdAsync(id);
        if (existingContactInfo is null) 
        {
            var exception = new BusinessRuleException(
                message: ContactInfoBusinessRuleErrorMessages.ContactInfoNotFound
            );
            return Result.Failure(
                error: exception,
                message: ContactInfoBusinessRuleMessages.contactInfoBusinessRulesOperationCompletedNotSuccessfully,
                statusCode: (int)HttpStatusCode.NotFound
            );
        }
        return Result.Success();
    }

    public async Task<Result> CheckIfEmailAlreadyExistsAsync(string email)
    {
        var existingContactInfo = await contactInfoReadRepository.GetSingleAsync(
            x => x.Email.ToLower() == email.ToLower()
        );

        if (existingContactInfo != null)
        {
            var exception = new BusinessRuleException(
                message: ContactInfoBusinessRuleErrorMessages.EmailAlreadyExists
            );
            return Result.Failure(
                error: exception,
                message: ContactInfoBusinessRuleMessages.contactInfoBusinessRulesOperationCompletedNotSuccessfully,
                statusCode: (int)HttpStatusCode.Conflict
            );
        }

        return Result.Success();
    }

    public async Task<Result> CheckIfPhoneAlreadyExistsAsync(string phone)
    {
        var normalizedPhone = NormalizePhone(phone);
        var existingContactInfo = await contactInfoReadRepository
            .GetSingleAsync(x => NormalizePhone(x.Phone) == normalizedPhone);

        if (existingContactInfo != null)
        {
            var exception = new BusinessRuleException(
                message: ContactInfoBusinessRuleErrorMessages.PhoneAlreadyExists
            );
            return Result.Failure(
                error: exception,
                message: ContactInfoBusinessRuleMessages.contactInfoBusinessRulesOperationCompletedNotSuccessfully, 
                statusCode: (int)HttpStatusCode.Conflict
            );
        }

        return Result.Success();
    }

    private string NormalizePhone(string phone)
    {
        return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
    }
}