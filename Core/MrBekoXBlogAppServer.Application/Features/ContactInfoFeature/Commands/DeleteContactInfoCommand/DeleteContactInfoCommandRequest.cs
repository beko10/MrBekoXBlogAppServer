using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Commands.DeleteContactInfoCommand;

public class DeleteContactInfoCommandRequest : IRequest<DeleteContactInfoCommandResponse>
{
    public string? Id { get; set; }
}
