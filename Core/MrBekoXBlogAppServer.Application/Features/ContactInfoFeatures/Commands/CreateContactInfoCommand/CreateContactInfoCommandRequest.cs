using MediatR;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Commands.CreateContactInfoCommand;

public class CreateContactInfoCommandRequest : IRequest<CreateContactInfoCommandResponse>
{
    public CreateContactInfoDto? CreateContactInfoDtoRequest { get; set; }
}
