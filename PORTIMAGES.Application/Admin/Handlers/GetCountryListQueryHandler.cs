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
    public class GetCountryListQueryHandler:IRequestHandler<GetCountryListQuery, ApiResponse<List<CountryResponseDTO>>>
    {
        private readonly ICountryRepository _countryRepository;

        public GetCountryListQueryHandler(ICountryRepository countryRepository)
        {
            this._countryRepository = countryRepository;

        }

        public async Task<ApiResponse<List<CountryResponseDTO>>> Handle(GetCountryListQuery request, CancellationToken cancellationToken)
        {
            return await _countryRepository.GetCountryListAsync();
        }
    }
}
