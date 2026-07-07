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
    public class UpdateShipTypeCommandHandler : IRequestHandler<UpdateShipTypeCommand, ApiResponse<object>>
    {
        private readonly IShipTypeRepository _typeRepository;

        public UpdateShipTypeCommandHandler(IShipTypeRepository repository)
        {
            this._typeRepository = repository;
        }
        public async Task<ApiResponse<object>> Handle(UpdateShipTypeCommand request, CancellationToken cancellationToken)
        {
            var dto = new ShipTypeRequestDTO()
            {
                ID = request.ID,
                TypeName = request.TypeName,
                IsActive = request.IsActive,
                UpdatedBy = request.UpdatedBy
            };

            return await _typeRepository.UpdateShipTypeAsync(dto);
        }
    }
}
