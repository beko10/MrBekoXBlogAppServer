using MediatR;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Commands.CreateContactInfoCommand;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Commands.CreateContactInfoCommand;

public class CreateContactInfoCommandRequest : IRequest<CreateContactInfoCommandResponse>
{
    public CreateContactInfoCommandDto? CreateContactInfoDtoRequest { get; set; }
}
