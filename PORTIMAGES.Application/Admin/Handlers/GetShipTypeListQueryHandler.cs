using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetShipTypeListQueryHandler : IRequestHandler<GetShipTypeListQuery, ApiResponse<List<ShipTypeResponseDTO>>>
    {
        private readonly IShipTypeRepository _shipType;

        public GetShipTypeListQueryHandler(IShipTypeRepository typeRepository)
        {
            this._shipType = typeRepository;
        }
       public async Task<ApiResponse<List<ShipTypeResponseDTO>>> Handle(GetShipTypeListQuery request, CancellationToken cancellationToken)
       {
            return await _shipType.GetShipTypeListAsync();
       }
    }
}
