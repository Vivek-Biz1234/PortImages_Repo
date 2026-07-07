using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTIMAGES.Application.Admin.Interfaces
{
    public interface IINSStatusRepository
    {
        Task<ApiResponse<object>> AddINSStatusAsync(INSStatusRequestDTO request);

        Task<ApiResponse<List<INSStatusResponseDTO>>> GetINSStatusListAsync();

        Task<ApiResponse<INSStatusRequestDTO>> GetINSStatusByIdAsync(int id);

        Task<ApiResponse<object>> UpdateINSStatusAsync(INSStatusRequestDTO request);

        Task<ApiResponse<object>> DeleteINSStatusAsync(int id, int DeletedBy);
    }
}
