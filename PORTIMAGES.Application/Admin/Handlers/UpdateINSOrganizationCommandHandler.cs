using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
 

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class UpdateINSOrganizationCommandHandler : IRequestHandler<UpdateINSOrganizationCommand, ApiResponse<object>>
    {
        private readonly IINSOrganizationRepository _iINSOrganizationRepository;

        public UpdateINSOrganizationCommandHandler(IINSOrganizationRepository iINSOrganizationRepository)
        {
            this._iINSOrganizationRepository = iINSOrganizationRepository;
        }
       public async Task<ApiResponse<object>> Handle(UpdateINSOrganizationCommand request, CancellationToken cancellationToken)
       {
            var dto = new INSOrganizationRequestDTO()
            {
                ID = request.ID,
                OrganizationName = request.OrganizationName,
                IsActive = request.IsActive,
                UpdatedBy = request.UpdatedBy
            };

            return await _iINSOrganizationRepository.UpdateINSOrganizationAsync(dto);
        }
    }
}
