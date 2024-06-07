using Models.DTO;
using Models.DTO.Interfaces;
using Services.FND.Interfaces;
using Services.FND.PgBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FND
{
    public class LanguagesService: BasePageService<LanguagesDTO>, ILanguagesService
    {
        public LanguagesService(IServiceProvider serviceProvider): base("languages", serviceProvider)
        {

        }

        public override IEnumerable<LanguagesDTO> Index(int lang_id)
        {
            try
            {
                var lst = Index("languages", "*","");

                return lst;
            }
            catch 
            {
                throw;
            }
        }

        public override void create(LanguagesDTO dto, int lang_id)
        {
            if (dto == null)
                throw new ArgumentNullException();

            create(dto, lang_id );

        }

        public override bool delete(int id)
        {
            if (id == 0 || id == 1 || id==2)
                return false;

            return delete("languages", $"id={id}");
        }

        public override LanguagesDTO? getItem(int id, int lang_id)
        {
            try
            {
                var item = getItem("languages", "*", $"id={id}");

                return item;
            }catch
            {
                throw;
            }
        }

        public override void update(int id, IDto dto, int lang_id)
        {

            if (dto == null)
               throw new ArgumentException();

            var content = dto as LanguagesDTO;

            if (content == null)
                throw new InvalidCastException();

            try
            {
                update<LanguagesDTO>(id, "languages", content, "", lang_id);
            }
            catch
            {
                throw;
            }

        }
    }
}
