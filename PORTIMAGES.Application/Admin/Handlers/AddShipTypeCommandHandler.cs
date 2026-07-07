using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class AddShipTypeCommandHandler : IRequestHandler<AddShipTypeCommand, ApiResponse<object>>
    {
        private readonly IShipTypeRepository _shipTypeRepository;

        public AddShipTypeCommandHandler(IShipTypeRepository shipTypeRepository)
        {
            this._shipTypeRepository = shipTypeRepository;
        }
       public async Task<ApiResponse<object>> Handle(AddShipTypeCommand request, CancellationToken cancellationToken)
       {
            var dto = new ShipTypeRequestDTO()
            {
                TypeName = request.TypeName,
                IsActive = request.IsActive,
                CreatedBy = request.CreatedBy
            };

            return await _shipTypeRepository.AddShipTypeAsync(dto);
        }
    }
}
