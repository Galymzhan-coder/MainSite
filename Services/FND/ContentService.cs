using Models.DTO;
using Models.DTO.Interfaces;
using Services.FND.Interfaces;
using Services.FND.PgBase;

namespace Services.FND
{
    public class ContentService: BasePageService<ContentDTO>, IContentService
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
                var item = getItem("content c, content_translations ct", "c.id, ct.title , ct.text, c.sefname , c.is_active , c.create_date , c.update_date ,ct.meta_description , ct.meta_keywords,  ct.meta_title", $"c.id =ct.content_id and c.id={id} and ct.lang_id={lang_id}");

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

                var contentTranslationsDTO = new ContentTranslationsDTO() { content_id = id, lang_id = lang_id, title = content.title??"", text = content.text??"", meta_description = content.meta_description??"", meta_keywords = content.meta_keywords??"", meta_title = content.meta_title??"" };

                update<ContentTranslationsDTO>("content_translations", $" content_id ={id} and lang_id={lang_id}", contentTranslationsDTO, "");
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


            var idContent = create<ContentDTO>("content", dto, "id,title,text,meta_keywords,meta_title,meta_description", "id");


            ContentTranslationsDTO contentDTO = new ContentTranslationsDTO() { content_id = Convert.ToInt32(idContent["id"]), lang_id = lang_id, text =  content.text ?? "", title = content.title?? "",
                                                                        meta_description = content.meta_description ?? "", meta_keywords = content.meta_keywords ?? "", meta_title = content.meta_title ?? ""};

            create<ContentTranslationsDTO>("content_translations", contentDTO, "", "content_id,lang_id");
                        
        }

        public override bool delete(int id)
        {
            try
            {

                if (id < 0)
                    return false;

                if(delete("content_translations", $"content_id={id}"))
                {
                    return delete("content", $"id={id}");
                }

                return false;
                
            }
            catch
            {
                throw;
            }

        }

    }
}
