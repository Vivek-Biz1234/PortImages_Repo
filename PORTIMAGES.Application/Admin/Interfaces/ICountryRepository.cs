using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;


namespace PORTIMAGES.Application.Admin.Interfaces
{
    public interface ICountryRepository
    {
        Task<ApiResponse<object>> AddCountryAsync(CountryRequestDTO request);

        Task<ApiResponse<object>> UpdateCountryAsync(CountryRequestDTO request);

        Task<ApiResponse<object>> DeleteCountryAsync(int id, int DeletedBy);
        Task<ApiResponse<CountryRequestDTO?>> GetCountryByIdAsync(int id);
        Task<ApiResponse<List<CountryResponseDTO>>> GetCountryListAsync();


    }
}
