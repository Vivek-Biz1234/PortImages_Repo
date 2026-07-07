using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class AddINSOrganizationCommandHandler : IRequestHandler<AddINSOrganizationCommand, ApiResponse<object>>
    {
        private readonly IINSOrganizationRepository _iINSOrganizationRepository;

        public AddINSOrganizationCommandHandler(IINSOrganizationRepository iINSOrganizationRepository)
        {
            this._iINSOrganizationRepository = iINSOrganizationRepository;
        }
       public async Task<ApiResponse<object>> Handle(AddINSOrganizationCommand request, CancellationToken cancellationToken)
       {
            var dto = new INSOrganizationRequestDTO()
            {
                OrganizationName = request.OrganizationName,
                IsActive = request.IsActive,
                CreatedBy = request.CreatedBy
            };

            return await _iINSOrganizationRepository.AddINSOrganizationAsync(dto);
        }
    }
}
