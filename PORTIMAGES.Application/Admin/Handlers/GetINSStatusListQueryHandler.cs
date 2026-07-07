using MediatR;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Application.Admin.Queries;
using PORTIMAGES.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTIMAGES.Application.Admin.Handlers
{
    public class GetINSStatusListQueryHandler : IRequestHandler<GetINSStatusListQuery, ApiResponse<List<INSStatusResponseDTO>>>
    {
        private readonly IINSStatusRepository _iNSStatusRepository;
         
        public GetINSStatusListQueryHandler(IINSStatusRepository iNSStatusRepository)
        {
            this._iNSStatusRepository = iNSStatusRepository;
        }
       public async Task<ApiResponse<List<INSStatusResponseDTO>>> Handle(GetINSStatusListQuery request, CancellationToken cancellationToken)
       {
            return await _iNSStatusRepository.GetINSStatusListAsync();
       }
    }
}
