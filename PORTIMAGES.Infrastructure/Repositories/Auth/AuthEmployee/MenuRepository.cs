using Dapper; 
using PORTIMAGES.Application.Auth.AuthEmployee.DTOs;
using PORTIMAGES.Application.Auth.AuthEmployee.Interfaces;
using PORTIMAGES.Infrastructure.Persistence;
using System.Data;

namespace PORTIMAGES.Infrastructure.Repositories.Auth.AuthEmployee
{
    public class MenuRepository:IMenuRepository
    {
        private readonly IDapperRepository _dapper;
        public MenuRepository(IDapperRepository dapper)
        {
                this._dapper = dapper;
        } 
        public async Task<List<MainMenuDTO>> GetMenuByUserIdAsync(int empId)
        {
            var parameters = new { EmpId = empId };

            using var multi = await _dapper.QueryMultipleAsync("usp_get_sidebar_menu_by_emp",parameters,CommandType.StoredProcedure);

            // First Result Set → Main Menus
            var mainMenus = (await multi.ReadAsync<MainMenuDTO>()).ToList();

            // Second Result Set → Sub Menus
            var subMenus = (await multi.ReadAsync<SubMenuDTO>()).ToList();

            // Group SubMenus into MainMenus
            foreach (var main in mainMenus)
            {
                main.SubMenus = subMenus
                    .Where(x => x.MainMenuId == main.MainMenuId)
                    .ToList();
            }

            return mainMenus;
        } 
    }
}
