using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class UpdateInventoryStatusCommandHandler:IRequestHandler<UpdateInventoryStatusCommand,ApiResponse<object>>
    {
        private readonly IInventoryStatusRepository _inventoryStatusRepository;
        public UpdateInventoryStatusCommandHandler(IInventoryStatusRepository inventoryStatusRepository)
        {
            this._inventoryStatusRepository = inventoryStatusRepository;
        }

        public async Task<ApiResponse<object>> Handle(UpdateInventoryStatusCommand request,CancellationToken cancellationToken)
        {
            var dto = new InventoryStatusRequestDTO()
            {
                ID = request.ID,
                StatusName = request.StatusName,
                IsActive = request.IsActive,
                UpdatedBy = request.UpdatedBy
            };
            return await _inventoryStatusRepository.UpdateInventoryStatusAsync(dto);
        }
    }
}
