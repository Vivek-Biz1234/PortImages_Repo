using PORTIMAGES.Application.Admin.DTOs;
using PORTIMAGES.Common.Responses;

namespace PORTIMAGES.Application.Admin.Interfaces
{
    public interface IEmployeeMasterRepository
    {
        Task<ApiResponse<object>> AddEmployeeAsync(EmployeeMasterRequestDTO request);
        Task<ApiResponse<object>> UpdateEmployeeAsync(EmployeeMasterRequestDTO request);
        Task<ApiResponse<object>> DeleteEmployeeAsync(int id, int DeletedBy);
        Task<ApiResponse<EmployeeMasterRequestDTO?>> GetEmployeeByIdAsync(int id);
        Task<ApiResponse<List<EmployeeMasterResponseDTO>>> GetEmployeeListAsync(int _id=0);
        Task<ApiResponse<object>> ChangePasswordAsync(ChangeEmpPasswordDTO req);
    }
}
