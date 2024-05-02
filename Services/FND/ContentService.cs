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
    public class ContentService: DBService<ContentDTO>, IContentService
    {
        private ODDANP _odp;

        public ContentService(ODDANP odp): base(odp, "content") 
        { 
            _odp = odp;
        }

        public override IEnumerable<ContentDTO> Index()
        {
            try
            {
                var lst = Index("");
                return lst;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public override ContentDTO? getItem(int id)
        {
            try
            {
                var item = getItem(id);

                return item;
            }
            catch
            {
                throw new NotImplementedException();
            }
            
        }

    }
}
