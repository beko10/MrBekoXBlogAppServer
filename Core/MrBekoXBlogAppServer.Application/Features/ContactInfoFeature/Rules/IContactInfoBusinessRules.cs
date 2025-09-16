using MrBekoXBlogAppServer.Application.Common.Results;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Rules;

public interface IContactInfoBusinessRules
{
    Task<Result> CheckIfEmailAlreadyExistsAsync(string email);
    Task<Result> CheckIfPhoneAlreadyExistsAsync(string phone);
    Task<Result> CheckIfContactInfoAlreadyExistsAsync();
    Task<Result> CheckIfContactInfoExistsByIdAsync(string id);
}
