using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class UpdateCategoryCommandHandler:IRequestHandler<UpdateCategoryCommand, ApiResponse<object>>
    {
        private readonly ICategoryRepository _categoryRepository;
    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        this._categoryRepository = categoryRepository;
    }

    public async Task<ApiResponse<object>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var dto = new CategoryRequestDTO
        {
            ID = request.ID,
            CategoryName = request.CategoryName,
            Titletag = request.Titletag,
            KeywordTag = request.KeywordTag,
            Description = request.Description,
            IsActive = request.IsActive,
            UpdatedBy = request.UpdatedBy,
        };
        return await _categoryRepository.UpdateCategoryAsync(dto);
    }

}
}
