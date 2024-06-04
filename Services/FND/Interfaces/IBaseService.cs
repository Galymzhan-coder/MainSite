using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FND.Interfaces
{
    public interface IBaseService<T>
    {
        IEnumerable<T> Index();
        //IEnumerable<T> Index(string whereCond);
        void create();
        void update(int id, T dto);
        void delete(int id);
        T? getItem(int id, string whereCond);
    }
}
