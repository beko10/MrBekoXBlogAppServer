using MediatR;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Commands.CreateCategoryCommand;

public class CreateCategoryCommandRequest : IRequest<CreateCategoryCommandResponse>
{
    public CreateCommandCategoryDto? CreateCommandCategoryDtoRequest { get; set; }
}
