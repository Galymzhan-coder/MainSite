using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FND.Interfaces
{
    public interface IBasePageIndexHierarchyService<T> : IBasePageService<T>
    {
        IEnumerable<T> IndexHierarchySorted(string tableName, string fields, string parent_id_col, string id_col, string indented_field, string indent_symbol, string indent_end_symbol, string whereConditions);
    }
}
