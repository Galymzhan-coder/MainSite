using Models.DTO;
using Models.DTO.Interfaces;
using Models.DTO.ResultDTOs;
using Services.FND.Interfaces;
using Services.FND.PgBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FND
{
    public class BlogCategoriesService : BasePageService<BlogCategoriesDTO>, IBlogCategoriesService
    {
        public BlogCategoriesService(IServiceProvider serviceProvider)
        :base("blog_categories", serviceProvider)
        { 
        }

        public override IEnumerable<BlogCategoriesDTO> Index(int lang_id)
        {
            try
            {
                var lst = Index("blog_categories bc, blog_categories_translations bct", @"bc.id, COALESCE(bct.title , bc.title) title, bc.sefname, bc.is_active", 
                                $"bc.id=bct.category_id and bct.lang_id={lang_id}");
                return lst;
            }
            catch
            {
                throw;
            }
        }


        public override BlogCategoriesDTO? getItem(int id, int lang_id)
        {
            try
            {
                var item = getItem("blog_categories bc, blog_categories_translations bct", @"bc.id, COALESCE(bct.title , bc.title) title, bc.sefname, bc.is_active, bc.create_date, bc.update_date", 
                                    $"bc.id = bct.category_id and bc.id={id} and bct.lang_id={lang_id}");

                return item;
            }
            catch
            {
                throw;
            }
        }

        public override void update(int id, IDto dto, int lang_id)
        {
            var blogCategories = dto as BlogCategoriesDTO;

            if (blogCategories == null)
                return;

            try
            {

                update<BlogCategoriesDTO>(id, "blog_categories", blogCategories, "id, create_date", lang_id);

                var blogCategoriesTranslationsDTO = new BlogCategoriesTranslationsDTO() { category_id = id, lang_id = lang_id, title = blogCategories.title ?? "" };

                update<BlogCategoriesTranslationsDTO>("blog_categories_translations", $" category_id ={id} and lang_id={lang_id}", blogCategoriesTranslationsDTO, "");
            }
            catch
            {
                throw;
            }
        }

        public override void create(BlogCategoriesDTO dto, int lang_id)
        {
            if (dto == null)
                return;

            var blogCategories = dto as BlogCategoriesDTO;


            var idContent = create<BlogCategoriesDTO>("blog_categories", dto, "id", "id");


            BlogCategoriesTranslationsDTO blogCategoriesDTO = new BlogCategoriesTranslationsDTO()
            {
                category_id = Convert.ToInt32(idContent["id"]),
                lang_id = lang_id,
                title = blogCategories.title ?? "",
            };

            create<BlogCategoriesTranslationsDTO>("blog_categories_translations", blogCategoriesDTO, "", "category_id,lang_id");

        }

        public override bool delete(int id)
        {
            try
            {

                if (id < 0)
                    return false;

                if (delete("blog_categories_translations", $"category_id={id}"))
                {
                    return delete("blog_categories", $"id={id}");
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
