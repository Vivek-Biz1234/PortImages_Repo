using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class AddInventoryStatusCommandHandler:IRequestHandler<AddInventoryStatusCommand,ApiResponse<object>>
    {
        private readonly IInventoryStatusRepository _inventoryStatusRepository;
        public AddInventoryStatusCommandHandler(IInventoryStatusRepository inventoryStatusRepository)
        {
            this._inventoryStatusRepository = inventoryStatusRepository;
        }
        public async Task<ApiResponse<object>> Handle(AddInventoryStatusCommand request,CancellationToken cancellationToken)
        {
            var dto = new InventoryStatusRequestDTO()
            {
                StatusName = request.StatusName,
                IsActive = request.IsActive,
                CreatedBy = request.CreatedBy
            };
            return await _inventoryStatusRepository.AddInventoryStatusAsync(dto);
        }
    }
}
