using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetInventoryStatusByIdQueryHandler:IRequestHandler<GetInventoryStatusByIdQuery,ApiResponse<InventoryStatusRequestDTO?>>
    {
        private readonly IInventoryStatusRepository _inventoryStatusRepository;
        public GetInventoryStatusByIdQueryHandler(IInventoryStatusRepository inventoryStatusRepository)
        {
            this._inventoryStatusRepository = inventoryStatusRepository;
        }
        public async Task<ApiResponse<InventoryStatusRequestDTO?>> Handle(GetInventoryStatusByIdQuery request,CancellationToken cancellationToken)
        {
            return await _inventoryStatusRepository.GetInventoryStatusByIdAsync(request.Id);
        }
    }
}
