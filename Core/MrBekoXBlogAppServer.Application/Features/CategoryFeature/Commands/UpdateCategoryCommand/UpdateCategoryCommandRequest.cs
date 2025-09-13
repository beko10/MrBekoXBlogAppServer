using MediatR;
using MrBekoXBlogAppServer.Application.Features.CategoryFeature.DTOs;

namespace MrBekoXBlogAppServer.Application.Features.CategoryFeature.Commands.UpdateCategoryCommand;

public class UpdateCategoryCommandRequest : IRequest<UpdateCategoryCommandResponse>
{
    public UpdateCommandCategoryDto? UpdateCommandCategoryDtoRequest { get; set; }
}
