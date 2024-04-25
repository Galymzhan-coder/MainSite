using Models.DTO;
using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FND.Interfaces
{
    public interface IDBService<T> : IBaseService<T> where T : IDto
    {
        abstract List<T> getHierarchyLst();

        abstract T? getItem(int id);
        
    }
}
