using MediatR;
using PORTIMAGES.Application.User.DTOs;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.User.Queries
{
    public  class GetMyProductListQuery: IRequest<ApiResponse<List<MyProductResponseDTOs>>>
    {
        public int ClientId { get; set; } 
        public int? TerminalID { get; set; } 
        public int? InsOrganizationID { get; set; } 
        public int? InsDestinationId { get; set; }
        public int? InventoryStatusId { get; set; }
        public int? ModelId { get; set; }
        public int? VehicleStatusId { get; set; }
        public int? InsStatusId { get; set; }
        public int? ShipId { get; set; } 
        public string? YardInDate { get; set; }
        public string? YardOutDate { get; set; }
        public string? InsDateFrom { get; set; }
        public string? InsDateTo { get; set; }
        public string? ChassisNo { get; set; }
        public string? VoyageNo { get; set; }
        public int? ContainerNo { get; set; }
    }
}
