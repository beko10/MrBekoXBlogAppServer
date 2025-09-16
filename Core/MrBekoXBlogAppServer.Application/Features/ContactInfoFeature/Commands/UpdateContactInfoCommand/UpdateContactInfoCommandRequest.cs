using MediatR;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Commands.UpdateContactInfoCommand;

public class UpdateContactInfoCommandRequest : IRequest<UpdateContactInfoCommandResponse>
{
    public UpdateContactInfoCommandDto? UpdateContactInfoDtoRequest { get; set; }
}
