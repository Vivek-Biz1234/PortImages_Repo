using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Queries
{
    public class GetShipUseListByIdQuery : IRequest<ApiResponse<ShipUseRequestDTO?>?>
    {
        public int Id { get; set; }
        public GetShipUseListByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}
