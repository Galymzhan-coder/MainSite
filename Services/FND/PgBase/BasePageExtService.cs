using Models.DTO.Interfaces;
using Models.DTO.ResultDTOs;
using Services.FND.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FND.PgBase
{
    public abstract class BasePageExtService<T>: BasePageService<T>, IBasePageExtService<T> where T : IDto, new ()
    {
        public BasePageExtService(string tableName, IServiceProvider serviceProvider): base(tableName, serviceProvider) 
        { 
        
        }

        public abstract PaginatedResult<T> IndexPaginated(int page_num, int page_size, int lang_id);

        public PaginatedResult<T> IndexPaginated(string table, string fields, int page_num, int page_size, string whereCond, string orderBy)
        {
            
            var cnt = _odp.Routine.QueryAsResultArray(table, "count(*)", whereCond, ref error).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(error))
                throw new Exception(error);

            var sql = _cmd_select.BuildSelectPaginatedCMD($"{db_prefix}{table}", fields, page_num, page_size, whereCond, orderBy);


            var lst = _odp.Routine.GetFromDatabase<T>(ref error, sql);

            if (!string.IsNullOrWhiteSpace(error))
                throw new Exception(error);

            return new PaginatedResult<T>
            {
                Items = lst.ToList<T>(),
                TotalPages = Convert.ToInt32(cnt)
            };
        }
    }
}
