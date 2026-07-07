using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Queries
{
    public class GetInventoryStatusByIdQuery:IRequest<ApiResponse<InventoryStatusRequestDTO?>?>
    {
        public int Id { get; set; }
        public GetInventoryStatusByIdQuery(int id)
        {
                this.Id = id;
        }
    }
}
