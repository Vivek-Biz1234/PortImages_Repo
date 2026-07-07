using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses; 

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class AddCategoryCommandHandler:IRequestHandler<AddCategoryCommand, ApiResponse<object>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public AddCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<ApiResponse<object>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var dto = new CategoryRequestDTO()
            {

                CategoryName = request.CategoryName,
                Titletag = request.Titletag,
                KeywordTag = request.KeywordTag,
                Description = request.Description,
                IsActive = request.IsActive,
                CreatedBy = request.CreatedBy,
            };
            return await _categoryRepository.AddCategoryAsync(dto);
        }
    }
}
