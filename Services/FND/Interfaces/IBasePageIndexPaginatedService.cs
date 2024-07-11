using Models.DTO.Interfaces;
using Models.DTO.ResultDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FND.Interfaces
{
    public interface IBasePageIndexPaginatedService<T> : IBasePageService<T>
    {
        PaginatedResult<T> IndexPaginated(string table, string fields, int page_num, int page_size, string whereCond, string orderBy);

    }
}
