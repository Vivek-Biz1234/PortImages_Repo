using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Queries
{
    public class GetShipUseListQuery : IRequest<ApiResponse<List<ShipUseResponseDTO>>>
    {
    }
}
