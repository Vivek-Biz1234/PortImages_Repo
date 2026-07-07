using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses; 
namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetCategoryListQueryHandler:IRequestHandler<GetCategoryListQuery, ApiResponse<List<CategoryResponseDTO>>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryListQueryHandler(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;

        }
        public async Task<ApiResponse<List<CategoryResponseDTO>>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetCategoryListAsync();
        }
    }
}
