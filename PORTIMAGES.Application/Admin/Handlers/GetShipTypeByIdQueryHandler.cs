using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetShipTypeByIdQueryHandler : IRequestHandler<GetShipTypeByIdQuery, ApiResponse<ShipTypeRequestDTO?>>
    {
        private readonly IShipTypeRepository _typeRepository;

        public GetShipTypeByIdQueryHandler(IShipTypeRepository typeRepository)
        {
            this._typeRepository = typeRepository;
        }
        public async Task<ApiResponse<ShipTypeRequestDTO?>> Handle(GetShipTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _typeRepository.GetShipTypeStatusByIdAsync(request.Id);
        }
    } 
}
