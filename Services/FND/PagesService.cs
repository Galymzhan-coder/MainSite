using Models.DTO;
using Models.FFIFND;
using Services.FND.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FND
{
    public class PagesService:DBService<PagesDTO>, IPagesService
    {
        public PagesService(ODDANP odp) : base(odp,"pages")
        { }

        public override IEnumerable<PagesDTO> Index()
        {

            //var lst = Index($"{db_prefix}categories", " 1=1 order by root,lft,rgt");
            var lst = Index("");

            return lst;
        }

        public override PagesDTO? getItem(int id)
        {
            var item = getItem(id, "");
            return item;
        }
    }
}
