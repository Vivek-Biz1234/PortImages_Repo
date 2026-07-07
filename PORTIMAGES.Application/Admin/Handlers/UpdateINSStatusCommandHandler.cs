using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class UpdateINSStatusCommandHandler : IRequestHandler<UpdateINSStatusCommand, ApiResponse<object>>
    {
        private readonly IINSStatusRepository _iNSStatusRepository;


        public UpdateINSStatusCommandHandler(IINSStatusRepository iNSStatusRepository)
        {
            this._iNSStatusRepository = iNSStatusRepository;
        }
        public async Task<ApiResponse<object>> Handle(UpdateINSStatusCommand request, CancellationToken cancellationToken)
        {
            var dto = new INSStatusRequestDTO()
            {
                ID = request.ID,
                StatusName = request.StatusName,
                IsActive = request.IsActive,
                UpdatedBy = request.UpdatedBy
            };
            return await _iNSStatusRepository.UpdateINSStatusAsync(dto);
        }
    }
}
