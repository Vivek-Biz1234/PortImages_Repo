using Dapper;
using Microsoft.Extensions.Logging;
using PORTIMAGES.Application.Ship.DTOs.PORTIMAGES.Application.Ship.DTOs;
using PORTIMAGES.Application.Ship.DTOs;
using PORTIMAGES.Application.Ship.Interfaces;
using PORTIMAGES.Common.Responses;
using PORTIMAGES.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTIMAGES.Infrastructure.Repositories.Admin
{
    public class ShippingRepository : IShippingRepository
    {
        private readonly IDapperRepository _dapper;
        private readonly ILogger<ShippingRepository> _logger;

        public ShippingRepository(IDapperRepository dapper, ILogger<ShippingRepository> logger)
        {
            _dapper = dapper;
            _logger = logger;
        }

        #region Add
        public async Task<ApiResponse<object>> AddShippingAsync(ShippingRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CountryId", request.CountryId);
                param.Add("@ShipId", request.ShipId);
                param.Add("@ShippingName", request.ShippingName);
                param.Add("@Email", request.Email);
                param.Add("@Contact", request.Contact);
                param.Add("@Fax", request.Fax);
                param.Add("@PasswordHash", request.Contact);
                param.Add("@CCMail", request.CCMail);
                param.Add("@HOAddress", request.HOAddress);
                param.Add("@BOAddress", request.BOAddress);
                param.Add("@PersonInCharge", request.PersonInCharge);
                param.Add("@Rate", request.Rate);
                param.Add("@OpeningBal", request.OpeningBal);
                param.Add("@IsActive", request.IsActive);
                param.Add("@CreatedBy", request.CreatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_add_shipping", param, CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Shipping added successfully !!"),
                    2 => new ApiResponse<object>(2, "Email id already exists !!"),
                    3 => new ApiResponse<object>(3, "Contact number already exists !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString()[..8];
                //_logger.LogError(ex, "AddShipping failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99,"Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region Update
        public async Task<ApiResponse<object>> UpdateShippingAsync(ShippingRequestDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", request.ID);
                param.Add("@CountryId", request.CountryId);
                param.Add("@ShipId", request.ShipId);
                param.Add("@ShippingName", request.ShippingName);
                param.Add("@Email", request.Email);
                param.Add("@Contact", request.Contact);
                param.Add("@Fax", request.Fax);
                param.Add("@CCMail", request.CCMail);
                param.Add("@HOAddress", request.HOAddress);
                param.Add("@BOAddress", request.BOAddress);
                param.Add("@PersonInCharge", request.PersonInCharge);
                param.Add("@Rate", request.Rate);
                param.Add("@OpeningBal", request.OpeningBal);
                param.Add("@IsActive", request.IsActive);
                param.Add("@UpdatedBy", request.UpdatedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_update_shipping", param, CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Shipping updated successfully !!"),
                    2 => new ApiResponse<object>(2, "Email id already exists !!"),
                    3 => new ApiResponse<object>(3, "Contact number already exists !!"),
                    -1 => new ApiResponse<object>(-1, "Shipping not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString()[..8];
                //_logger.LogError(ex, "UpdateShipping failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99,"Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region Delete
        public async Task<ApiResponse<object>> DeleteShippingAsync(int id, int deletedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@DeletedBy", deletedBy);
                param.Add("@Status", dbType: DbType.Int16, direction: ParameterDirection.Output);

                await _dapper.ExecuteAsync("dbo.usp_delete_shipping", param, CommandType.StoredProcedure);

                short result = param.Get<short?>("@Status") ?? -99;

                return result switch
                {
                    1 => new ApiResponse<object>(1, "Shipping deleted successfully !!"),
                    -1 => new ApiResponse<object>(-1, "Shipping not found !!"),
                    _ => new ApiResponse<object>(-99, "Something went wrong !!")
                };
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString()[..8];
                //_logger.LogError(ex, "DeleteShipping failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<object>(-99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region GetById
        public async Task<ApiResponse<ShippingRequestDTO?>> GetShippingByIdAsync(int id)
        {
            try
            {
                var data = await _dapper.QueryFirstOrDefaultAsync<ShippingRequestDTO>(
                    "dbo.usp_get_shipping_by_id",
                    new { ID = id },
                    CommandType.StoredProcedure);

                if (data == null)
                    return new ApiResponse<ShippingRequestDTO?>(-1, "Shipping not found !!", null);

                return new ApiResponse<ShippingRequestDTO?>(1, "Success", data);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString()[..8];
                //_logger.LogError(ex, "GetShippingById failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<ShippingRequestDTO?>(-99,
                    "Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion

        #region List
        public async Task<ApiResponse<List<ShippingResponseDTO>>> GetShippingListAsync()
        {
            try
            {
                var data = await _dapper.QueryAsync<ShippingResponseDTO>("dbo.usp_get_shipping_list",null,CommandType.StoredProcedure);
                return new ApiResponse<List<ShippingResponseDTO>>(1, "Success", data.ToList());
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString()[..8];
                //_logger.LogError(ex, "GetShippingList failed | ErrorId: {ErrorId}", errorId);
                return new ApiResponse<List<ShippingResponseDTO>>(-99,"Something went wrong.<br/>Please contact support with Error ID: " + errorId);
            }
        }
        #endregion
    }
}
