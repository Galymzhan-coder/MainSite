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
    public class FaqCategoriesService:BasePageService<FaqCategoriesDTO>, IFaqCategoriesService
    {
        public FaqCategoriesService(IServiceProvider serviceProvider) : base("faq_categories", serviceProvider)
        {

        }

        public override IEnumerable<FaqCategoriesDTO> Index(int lang_id)
        {
            try
            {
                var lst = Index("faq_categories", "*", "");

                return lst;
            }
            catch
            {
                throw;
            }
        }

        public override void create(FaqCategoriesDTO dto, int lang_id)
        {
            if (dto == null)
                throw new ArgumentNullException();

            create(dto, lang_id);

        }

        public override bool delete(int id)
        {
            if (id == 0 || id == 1 || id == 2)
                return false;

            return delete("faq_categories", $"id={id}");
        }

        public override FaqCategoriesDTO? getItem(int id, int lang_id)
        {
            try
            {
                var item = getItem("faq_categories", "*", $"id={id}");

                return item;
            }
            catch
            {
                throw;
            }
        }

        public override void update(int id, IDto dto, int lang_id)
        {

            if (dto == null)
                throw new ArgumentException();

            var content = dto as FaqCategoriesDTO;

            if (content == null)
                throw new InvalidCastException();

            try
            {
                update<FaqCategoriesDTO>(id, "faq_categories", content, "", lang_id);
            }
            catch
            {
                throw;
            }

        }
    }
}
