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
    public class AddINSStatusCommandHandler : IRequestHandler<AddINSStatusCommand, ApiResponse<object>>
    {
        private readonly IINSStatusRepository _iNSStatusRepository;

        public AddINSStatusCommandHandler(IINSStatusRepository statusRepository)
        {
            this._iNSStatusRepository = statusRepository;
        }
       public async Task<ApiResponse<object>> Handle(AddINSStatusCommand request, CancellationToken cancellationToken)
       {
            var dto = new INSStatusRequestDTO()
            {
                StatusName = request.StatusName,
                IsActive = request.IsActive,
                CreatedBy = request.CreatedBy
            };
            return await _iNSStatusRepository.AddINSStatusAsync(dto);

        }
    }
}
