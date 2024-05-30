using Models.DTO;
using Models.DTO.Interfaces;
using Models.FFIFND;
using Services.FND.Interfaces;
using Services.FND.PgBase;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FND
{
    public class ContentService: BasePageService<ContentDTO>, IContentService//DBService<ContentDTO>
    {

        public ContentService(IServiceProvider serviceProvider)
        : base("content", serviceProvider)
        {
        }


        public override IEnumerable<ContentDTO> Index(int lang_id)
        {
            try
            {
                var lst = Index("content c, content_translations ct", "c.id, ct.title , ct.text, c.sefname , c.is_active , c.create_date , c.update_date ,ct.meta_description , ct.meta_keywords, c.meta_keywords , ct.meta_title", $"c.id =ct.content_id and ct.lang_id={lang_id}");
                return lst;
            }
            catch
            {
                throw;
            }
        }

        public override ContentDTO? getItem(int id, int lang_id)
        {
            try
            {
                var item = getItem("content c, content_translations ct", "c.id, ct.title , ct.text, c.sefname , c.is_active , c.create_date , c.update_date ,ct.meta_description , ct.meta_keywords,  ct.meta_title", $"c.id =ct.content_id and ct.lang_id={lang_id}");

                return item;
            }
            catch
            {
                throw;
            }
            
        }

        public override void update(int id, IDto dto,int lang_id)
        {
            var content = dto as ContentDTO;
            if (content == null)
                return;

            try
            {
                
                update<ContentDTO>(id,"content", content, "title,text,meta_keywords,meta_title,meta_description", lang_id);

                update<ContentDTO>("content_translations", $" content_id ={id} and lang_id={lang_id}", content, "Id, sefname , is_active , create_date , update_date, meta_image");
            }
            catch
            {
                throw;
            }
        }

        public override void create(ContentDTO dto, int lang_id)
        {
            if (dto == null)
                return;

            var content = dto as ContentDTO;


            var idContent = create<ContentDTO>("content", dto, "title,text,meta_keywords,meta_title,meta_description", "id");


            ContentTranslationsDTO contentDTO = new ContentTranslationsDTO() { content_id = Convert.ToInt32(idContent["id"]), lang_id = lang_id, text =  content.text ?? "", title = content.title?? "",
                                                                        meta_description = content.meta_description ?? "", meta_keywords = content.meta_keywords ?? "", meta_title = content.meta_title ?? ""};

            create<ContentTranslationsDTO>("content_translations", contentDTO, "title,text,meta_keywords,meta_title,meta_description", "content_id,lang_id");

            throw new NotImplementedException();

        }

    }
}
