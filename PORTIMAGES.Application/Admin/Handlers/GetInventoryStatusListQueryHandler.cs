using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetInventoryStatusListQueryHandler : IRequestHandler<GetInventoryStatusListQuery, ApiResponse<List<InventoryStatusResponseDTO>>>
    {
        private readonly IInventoryStatusRepository _inventoryStatusRepository;
        public GetInventoryStatusListQueryHandler(IInventoryStatusRepository inventoryStatusRepository) 
        {
            this._inventoryStatusRepository = inventoryStatusRepository;
        }
        public async Task<ApiResponse<List<InventoryStatusResponseDTO>>> Handle(GetInventoryStatusListQuery request,CancellationToken cancellationToken)
        {
            return await _inventoryStatusRepository.GetInventoryStatusListAsync();
        }
    }
}
