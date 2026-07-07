using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetShipUseListQueryHandler : IRequestHandler<GetShipUseListQuery, ApiResponse<List<ShipUseResponseDTO>>>
    {
        private readonly IShipUseRepository shipUseRepository;

        public GetShipUseListQueryHandler(IShipUseRepository useRepository)
        {
            this.shipUseRepository = useRepository;
        }
       public async Task<ApiResponse<List<ShipUseResponseDTO>>> Handle(GetShipUseListQuery request, CancellationToken cancellationToken)
       {
            return await shipUseRepository.GetShipUseStatusListAsync();
       }
    }
}
