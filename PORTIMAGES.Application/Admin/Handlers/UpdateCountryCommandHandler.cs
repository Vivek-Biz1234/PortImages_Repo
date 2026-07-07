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
    public class UpdateCountryCommandHandler:IRequestHandler<UpdateCountryCommand, ApiResponse<object>>
    {
        private readonly ICountryRepository _countryRepository;
        public UpdateCountryCommandHandler(ICountryRepository countryRepository)
        {
            this._countryRepository = countryRepository;
        }

        public async Task<ApiResponse<object>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var dto = new CountryRequestDTO
            {
                ID = request.ID,
                CountryName = request.CountryName,
                IsActive = request.IsActive,
                UpdatedBy = request.UpdatedBy,
            };
            return await _countryRepository.UpdateCountryAsync(dto);
        }
    }
}
