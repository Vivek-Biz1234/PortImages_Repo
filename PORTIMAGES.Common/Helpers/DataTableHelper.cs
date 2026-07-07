using System.Data; 

namespace PORTIMAGES.Common.Helpers
{
    public static class DataTableHelper
    {
        public static DataTable ToDataTable<T>(List<T> data)
        {
            var table = new DataTable();

            var props = typeof(T).GetProperties(); 
            foreach (var prop in props)
            {
                table.Columns.Add(
                    prop.Name,
                    Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType
                );
            }
 
            foreach (var item in data)
            {
                var values = props
                    .Select(p => p.GetValue(item) ?? DBNull.Value)
                    .ToArray();

                table.Rows.Add(values);
            }

            return table;
        }
    }
}
