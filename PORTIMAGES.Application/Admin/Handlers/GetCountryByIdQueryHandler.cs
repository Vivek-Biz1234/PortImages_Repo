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
    public  class GetCountryByIdQueryHandler:IRequestHandler <GetCountryByIdQuery, ApiResponse<CountryRequestDTO?>>
    {
        private readonly ICountryRepository _countryRepository;
        public GetCountryByIdQueryHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public async Task<ApiResponse<CountryRequestDTO?>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _countryRepository.GetCountryByIdAsync(request.Id);
        }
    }
}
