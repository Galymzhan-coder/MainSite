using Models.DTO.Interfaces;
using Services.FND.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FND.PgBase
{
    public abstract class BasePageExtIndexHierarchyService<T> : BasePageService<T>, IBasePageIndexHierarchyService<T> where T : IDto, new()
    {
        public BasePageExtIndexHierarchyService(string tableName, IServiceProvider serviceProvider): base(tableName, serviceProvider) 
        { 
        
        }

        public abstract IEnumerable<T> IndexHierarchySorted(string indent_symbol, string indent_end_symbol, int lang);

        public IEnumerable<T> IndexHierarchySorted(string tableName, string fields, string parent_id_col, string id_col, string indented_field, string indent_symbol, string indent_pre_symbol, string whereConditions)
        {
            try
            {
                //id, parent_id, title, sefname, description, is_active, create_date, update_date, root, lft, rgt,level
                //var lst = Index("categories c , categories_translations ct", "c.id, c.parent_id, COALESCE(ct.title , c.title) title, sefname, COALESCE(ct.description , c.description) description, is_active, create_date, update_date, root, lft, rgt,level", $"c.id =ct.category_id and ct.lang_id={lang_id}");
                var sql = _cmd_select.BuildSelectHierarchySortedCMD(tableName, fields, parent_id_col, id_col, indented_field, indent_symbol, indent_pre_symbol, whereConditions);

                var lst = _odp.Routine.GetFromDatabase<T>(ref error, sql);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new Exception(error);

                return lst;
            }
            catch
            {
                throw;
            }
        }
    }
}
