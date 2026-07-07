using MediatR; 
using PORTIMAGES.Application.User.DTOs;
using PORTIMAGES.Application.User.Interfaces;
using PORTIMAGES.Application.User.Queries;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.User.Handlers
{
    public class GetMyProductListQueryHandler : IRequestHandler<GetMyProductListQuery, ApiResponse<List<MyProductResponseDTOs>>>
    {
        private readonly IUserProductRepository MyProductList;

        public GetMyProductListQueryHandler(IUserProductRepository productRepository)
        {
            this.MyProductList = productRepository;
        }
        public async Task<ApiResponse<List<MyProductResponseDTOs>>> Handle(GetMyProductListQuery request, CancellationToken cancellationToken)
        {
            var dto = new MyProductRequestDTOs
            {
                ClientId = request.ClientId,
                TerminalID = request.TerminalID,
                InsOrganizationID = request.InsOrganizationID,
                InsDestinationId = request.InsDestinationId,
                InventoryStatusId = request.InventoryStatusId,
                ModelId = request.ModelId,
                VehicleStatusId = request.VehicleStatusId,
                InsStatusId = request.InsStatusId,
                ShipId = request.ShipId,
                YardInDate = request.YardInDate,
                YardOutDate = request.YardOutDate,
                InsDateFrom = request.InsDateFrom,
                InsDateTo = request.InsDateTo,
                ChassisNo = request.ChassisNo,
                ContainerNo = request.ContainerNo,
                VoyageNo = request.VoyageNo
            };
            return await MyProductList.GetProductListAsync(dto);
        }
    }
}
