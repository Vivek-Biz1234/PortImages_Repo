using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class DeleteInventoryStatusCommandHandler:IRequestHandler<DeleteInventoryStatusCommand,ApiResponse<object>>
    {
        private readonly IInventoryStatusRepository _inventoryStatusRepository;
        public DeleteInventoryStatusCommandHandler(IInventoryStatusRepository inventoryStatusRepository)
        {
            this._inventoryStatusRepository = inventoryStatusRepository;
        }

        public async Task<ApiResponse<object>> Handle(DeleteInventoryStatusCommand request,CancellationToken cancellationToken)
        {
            return await _inventoryStatusRepository.DeleteInventoryStatusAsync(request.ID, request.DeletedBy);
        }
    }
}
