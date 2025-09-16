using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeatures.Commands.DeleteContactInfoCommand;

public class DeleteContactInfoCommandRequest : IRequest<DeleteContactInfoCommandResponse>
{
    public string? Id { get; set; }
}
