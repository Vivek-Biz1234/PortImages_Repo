using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses; 

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class AddINSDestionationCommandHandler : IRequestHandler<AddINSDestionationCommand, ApiResponse<object>>
    {
        private readonly IINSDestinationRepository _destionationrespository;

        public AddINSDestionationCommandHandler(IINSDestinationRepository destionationrespository)
        {
            this._destionationrespository = destionationrespository;
        }
        public async Task<ApiResponse<object>> Handle(AddINSDestionationCommand request, CancellationToken cancellationToken)
        {
            var dto = new INSDestinationRequestDTO()
            {
                DestinationName = request.DestinationName,
                IsActive = request.IsActive,
                CreatedBy = request.CreatedBy
            };

            return await _destionationrespository.AddINSDestinationAsync(dto);
        }
    }
}
