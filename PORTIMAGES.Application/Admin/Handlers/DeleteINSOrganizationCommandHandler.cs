using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class DeleteINSOrganizationCommandHandler : IRequestHandler<DeleteINSOrganizationCommand, ApiResponse<object>>
    {
        private readonly IINSOrganizationRepository _iINSOrganizationRepository;

        public DeleteINSOrganizationCommandHandler(IINSOrganizationRepository iINSOrganizationRepository)
        {
            this._iINSOrganizationRepository = iINSOrganizationRepository;
        }
        public async Task<ApiResponse<object>> Handle(DeleteINSOrganizationCommand request, CancellationToken cancellationToken)
        {
            return await _iINSOrganizationRepository.DeleteINSOrganizationAsync(request.ID, request.DeletedBy);
        }
    }
}
