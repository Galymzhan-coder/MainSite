using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO.ResultDTOs
{
    public class PaginatedResult<T>
    {
        public int TotalPages { get; set; }

        public List<T> Items { get; set; }
    }
}
