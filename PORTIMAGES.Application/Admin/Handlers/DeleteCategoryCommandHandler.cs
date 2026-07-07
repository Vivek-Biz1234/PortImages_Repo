using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class DeleteCategoryCommandHandler:IRequestHandler<DeleteCategoryCommand, ApiResponse<object>>
    {
        private readonly ICategoryRepository _subcategoryRepository;
        public DeleteCategoryCommandHandler(ICategoryRepository subcategoryRepository)
        {
            this._subcategoryRepository = subcategoryRepository;
        }

        public async Task<ApiResponse<object>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _subcategoryRepository.DeleteCategoryAsync(request.ID, request.DeletedBy);
        }
    }
}
