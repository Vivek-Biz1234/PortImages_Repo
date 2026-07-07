using MediatR;
using PORTIMAGES.Application.Admin.Commands;
using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Application.Admin.Interfaces;
using PORTIMAGES.Common.Responses; 

namespace PORTIMAGES.Application.Admin.Handlers
{
    public  class AddCountryCommandHandler: IRequestHandler<AddCountryCommand, ApiResponse<Object>>
    {
        private readonly ICountryRepository _countryRepository;
        public AddCountryCommandHandler(ICountryRepository countryRepository)
        {
            this._countryRepository = countryRepository;
        }
        public async Task<ApiResponse<object>> Handle(AddCountryCommand request, CancellationToken cancellationToken)
        {
            var dto = new CountryRequestDTO
            {
                CountryName = request.CountryName,
                IsActive = request.IsActive,
                CreatedBy = request.CreatedBy
            };
            return await _countryRepository.AddCountryAsync(dto);
        }
    }
} 
