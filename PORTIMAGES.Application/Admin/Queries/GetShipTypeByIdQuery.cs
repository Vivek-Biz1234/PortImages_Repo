using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Queries
{
    public class GetShipTypeByIdQuery : IRequest<ApiResponse<ShipTypeRequestDTO?>?>
    {
        public int Id { get; set; }
        public GetShipTypeByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}
