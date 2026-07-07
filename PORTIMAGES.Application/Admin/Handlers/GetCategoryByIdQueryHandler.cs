using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses; 

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetCategoryByIdQueryHandler:IRequestHandler<GetCategoryByIdQuery, ApiResponse<CategoryRequestDTO?>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<ApiResponse<CategoryRequestDTO?>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetCategoryByID(request.Id);
        }
    }
}
