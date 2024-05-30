using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FND.Interfaces
{
    public interface IBasePageService<T>
    {
        IEnumerable<T> Index(int lang_id);

        void create(T dto, int lang_id);
        //int create<T1>(string table, T1 dto, string excludeFields) where T1 : IDto;

        void update(int id, IDto dto, int lang_id);
        
        void delete(int id);

        T? getItem(int id, int lang_id);

        //T? getItem(int id, string whereCond);
    }
}
