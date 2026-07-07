using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class AddShipUseCommandHandler : IRequestHandler<AddShipUseCommand, ApiResponse<object>>
    {
        private readonly IShipUseRepository shipUseRepository;
        public AddShipUseCommandHandler(IShipUseRepository shipUse)
        {
            this.shipUseRepository = shipUse;
        }
       public async Task<ApiResponse<object>> Handle(AddShipUseCommand request, CancellationToken cancellationToken)
        {
            var dto = new ShipUseRequestDTO()
            {
                UseType = request.UseType,
                IsActive = request.IsActive,
                CreatedBy = request.CreatedBy
            };
            return await shipUseRepository.AddShipUseAsync(dto);
        }
    }
}
