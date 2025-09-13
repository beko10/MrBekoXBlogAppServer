using MediatR;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Commands.DeleteCategoryCommand;

public class DeleteCategoryCommandRequest : IRequest<DeleteCategoryCommandResponse>
{
    public string? Id { get; set; }
}
