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
    public class TopManagementService : BasePageService<TopManagementDTO>, ITopManagementService
    {
        public TopManagementService(IServiceProvider serviceProvider)
        : base("top_management", serviceProvider)
        {
        }


        public override IEnumerable<TopManagementDTO> Index(int lang_id)
        {
            try
            {
                var lst = Index("top_management c, top_management_translations ct", "c.id,COALESCE(ct.title,c.title) title , COALESCE(ct.text,c.text) as text, COALESCE(ct.full_name,c.full_name) full_name,c.image, c.image_description, c.position, c.weight, c.is_active , c.created_at , c.updated_at ", $"c.id =ct.manager_id and ct.lang_id={lang_id}");
                return lst;
            }
            catch
            {
                throw;
            }
        }

        public override TopManagementDTO? getItem(int id, int lang_id)
        {
            try
            {
                var item = getItem("top_management c, top_management_translations ct", "c.id, COALESCE(ct.title,c.title) title , COALESCE(ct.text,c.text) as text, COALESCE(ct.full_name,c.full_name) full_name,c.image, c.image_description, c.position, c.weight, c.is_active , c.created_at , c.updated_at ", $"c.id =ct.manager_id and c.id={id} and ct.lang_id={lang_id}");

                return item;
            }
            catch
            {
                throw;
            }

        }

        public override void update(int id, IDto dto, int lang_id)
        {
            var top_management = dto as TopManagementDTO;

            if (top_management == null)
                return;

            try
            {

                update<TopManagementDTO>(id, "top_management", top_management, "manager_id,title,text,full_name", lang_id);



                var TopManagementTranslationsDTO = new TopManagementTranslationsDTO() { manager_id = id, lang_id = lang_id, title = top_management.title ?? "", text = top_management.text ?? "", full_name = top_management.full_name ?? "" };

                update<TopManagementTranslationsDTO>("top_management_translations", $" manager_id ={id} and lang_id={lang_id}", TopManagementTranslationsDTO, "");
            }
            catch
            {
                throw;
            }
        }

        public override void create(TopManagementDTO dto, int lang_id)
        {
            if (dto == null)
                return;

            var top_management = dto as TopManagementDTO;


            var idTopManagement = create<TopManagementDTO>("top_management", dto, "id,title,text,full_name", "id");


            TopManagementTranslationsDTO TopManagementTranslationDTO = new TopManagementTranslationsDTO()
            {
                manager_id = Convert.ToInt32(idTopManagement["id"]),
                lang_id = lang_id,
                text = top_management.text ?? "",
                title = top_management.title ?? ""
            };

            create<TopManagementTranslationsDTO>("top_management_translations", TopManagementTranslationDTO, "", "manager_id,lang_id");

        }

        public override bool delete(int id)
        {
            try
            {

                if (id < 0)
                    return false;

                if (delete("top_management_translations", $"manager_id={id}"))
                {
                    return delete("top_management", $"id={id}");
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
