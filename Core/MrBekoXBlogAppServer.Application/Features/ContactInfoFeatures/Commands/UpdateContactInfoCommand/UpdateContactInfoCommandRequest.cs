using MediatR;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Commands.UpdateContactInfoCommand;

public class UpdateContactInfoCommandRequest : IRequest<UpdateContactInfoCommandResponse>
{
    public UpdateContactInfoDto? UpdateContactInfoDtoRequest { get; set; }
}
