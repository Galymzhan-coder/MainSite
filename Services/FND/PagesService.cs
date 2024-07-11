using Models.DTO;
using Models.DTO.Interfaces;
using Models.DTO.ResultDTOs;
using Models.Entities;
using Models.FFIFND;
using Services.FND.Interfaces;
using Services.FND.PgBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FND
{
    public class PagesService: BasePageExtIndexPaginatedService<PagesDTO>, IPagesService
    {
        public PagesService(IServiceProvider serviceProvider)
        : base("pages", serviceProvider)
        {
        }

        public override IEnumerable<PagesDTO> Index(int lang_id)
        {

            try
            {
                var lst = Index("pages p, pages_translations pt", @"p.id, p.user_id, p.category_id, pt.title, pt.short_text, pt.text, p.sefname, p.image, p.publish_date, p.views,p.is_active, p.create_date, 
                                p.update_date, pt.meta_description, pt.meta_keywords, p.meta_image, pt.meta_title, p.is_mainpage, p.download_file", $"p.id = pt.page_id and pt.lang_id={lang_id}");
                return lst;
            }
            catch
            {
                throw;
            }
        }

        public override PagesDTO? getItem(int id, int lang_id)
        {
            try
            {
                var item = getItem("pages p, pages_translations pt", @"p.id, p.user_id, p.category_id, pt.title, pt.short_text, pt.text, p.sefname, p.image, p.publish_date, p.views,p.is_active, p.create_date, 
                                   p.update_date, pt.meta_description, pt.meta_keywords, p.meta_image, pt.meta_title, p.is_mainpage, p.download_file", $"p.id = pt.page_id and p.id={id} and pt.lang_id={lang_id}");

                return item;
            }
            catch
            {
                throw;
            }

        }

        public override void update(int id, IDto dto, int lang_id)
        {
            var pages = dto as PagesDTO;

            if (pages == null)
                return;

            try
            {

                update<PagesDTO>(id, "pages", pages, "title,text,short_text,meta_keywords,meta_title,meta_description", lang_id);

                var pagesTranslationsDTO = new PagesTranslationsDTO() { page_id = id, lang_id = lang_id, title = pages.title ?? "", short_text = pages.short_text ?? "", text = pages.text ?? "", meta_description = pages.meta_description ?? "", meta_keywords = pages.meta_keywords ?? "", meta_title = pages.meta_title ?? "" };

                update<PagesTranslationsDTO>("pages_translations", $" page_id ={id} and lang_id={lang_id}", pagesTranslationsDTO, "");
            }
            catch
            {
                throw;
            }
        }

        public override void create(PagesDTO dto, int lang_id)
        {
            if (dto == null)
                return;

            var pages = dto as PagesDTO;


            var idContent = create<PagesDTO>("pages", dto, "id,title,text,short_text,meta_keywords,meta_title,meta_description", "id");


            PagesTranslationsDTO pagesDTO = new PagesTranslationsDTO()
            {
                page_id = Convert.ToInt32(idContent["id"]),
                lang_id = lang_id,
                text = pages.text ?? "",
                title = pages.title ?? "",
                short_text = pages.short_text ?? "",
                meta_description = pages.meta_description ?? "",
                meta_keywords = pages.meta_keywords ?? "",
                meta_title = pages.meta_title ?? ""
            };

            create<PagesTranslationsDTO>("pages_translations", pagesDTO, "", "page_id,lang_id");

        }

        public override bool delete(int id)
        {
            try
            {

                if (id < 0)
                    return false;

                if (delete("pages_translations", $"page_id={id}"))
                {
                    return delete("pages", $"id={id}");
                }

                return false;

            }
            catch
            {
                throw;
            }

        }
        //PaginatedResult<T> IndexPaginated(string table, string fields, int page_num, int page_size, string whereCond)
        public override PaginatedResult<PagesDTO> IndexPaginated(int pageNumber, int pageSize, int lang_id)
        {

            try
            {
                /*
                var lst = Index("pages p, pages_translations pt", @"p.id, p.user_id, p.category_id, pt.title, pt.short_text, pt.text, p.sefname, p.image, p.publish_date, p.views,p.is_active, p.create_date, 
                                p.update_date, pt.meta_description, pt.meta_keywords, p.meta_image, pt.meta_title, p.is_mainpage, p.download_file", $"p.id = pt.page_id and pt.lang_id={lang_id}");
                */
                var ret = IndexPaginated("pages p, pages_translations pt, categories c, categories_translations ct", @"p.id, p.user_id, p.category_id, pt.title, pt.short_text, pt.text, p.sefname, p.image, p.publish_date, p.views,p.is_active, p.create_date, 
                                p.update_date, pt.meta_description, pt.meta_keywords, p.meta_image, pt.meta_title, p.is_mainpage, p.download_file, COALESCE(ct.title , c.title) category", pageNumber, pageSize, $"p.id=pt.page_id and c.id=ct.category_id and p.category_id = c.id and ct.lang_id = pt.lang_id and pt.lang_id={lang_id}", "p.publish_date desc");

                return ret;
            }
            catch
            {
                throw;
            }
        }
    }
}
