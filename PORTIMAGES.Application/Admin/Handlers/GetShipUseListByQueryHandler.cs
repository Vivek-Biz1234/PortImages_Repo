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
    public class GetShipUseListByQueryHandler : IRequestHandler<GetShipUseListByIdQuery, ApiResponse<ShipUseRequestDTO?>>
    {
        private readonly IShipUseRepository useRepository;

        public GetShipUseListByQueryHandler(IShipUseRepository repository)
        {
            this.useRepository = repository;
        }
      public async Task<ApiResponse<ShipUseRequestDTO?>> Handle(GetShipUseListByIdQuery request, CancellationToken cancellationToken)
      {
            return await useRepository.GetShipUseStatusByIdAsync(request.Id);
      }
    }
}
