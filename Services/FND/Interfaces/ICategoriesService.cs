using Models.DTO;
using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FND.Interfaces
{
    public interface ICategoriesService : IDBIerarchyService<CategoryDTO>, IBaseService<CategoryDTO>
    {
        
    }
}
