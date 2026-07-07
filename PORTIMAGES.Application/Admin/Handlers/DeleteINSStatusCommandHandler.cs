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
    public class DeleteINSStatusCommandHandler : IRequestHandler<DeleteINSStatusCommand, ApiResponse<object>>
    {
        private readonly IINSStatusRepository _statusRepository;

        public DeleteINSStatusCommandHandler(IINSStatusRepository statusRepository)
        {
            this._statusRepository = statusRepository;
        }

       public async Task<ApiResponse<object>> Handle(DeleteINSStatusCommand request, CancellationToken cancellationToken)
       {
            return await _statusRepository.DeleteINSStatusAsync(request.ID, request.DeletedBy);
       }
    }
}
