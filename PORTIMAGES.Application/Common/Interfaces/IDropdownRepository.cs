using PORTIMAGES.Common.DTOs; 
namespace PORTIMAGES.Application.Common.Interfaces
{
    public interface IDropdownRepository
    {
        Task<IEnumerable<DropdownDTO>> GetAsync(string tableName,string valueField,string textField,string? filterField,int? filterValue,string? orderBy);
        Task<IEnumerable<DropdownDTO>> GetDropdownValuesAsync(string key, int? parentId);
    }
}
