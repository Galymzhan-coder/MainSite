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
    public class BlogsService : BasePageExtIndexPaginatedService<BlogsDTO>, IBlogsService
    {
        public BlogsService(IServiceProvider serviceProvider)
       : base("blogs", serviceProvider)
        {
        }
        public override IEnumerable<BlogsDTO> Index(int lang_id)
        {
            try
            {
                var lst = Index("blogs b, blogs_translations bt", @"b.id, b.category_id, COALESCE(bt.title , b.title) title, b.description, bt.text, b.sefname, b.image, b.publish_date, b.views,b.is_active, b.create_date, 
                                b.update_date, b.is_mainpage", $"b.id = bt.blog_id and bt.lang_id={lang_id}");
                return lst;
            }
            catch
            {
                throw;
            }
        }
        public override PaginatedResult<BlogsDTO> IndexPaginated(int pageNumber, int pageSize, int lang_id, string whereCond="")
        {
            try
            {
                var ret = IndexPaginated("blogs b, blogs_translations bt, blog_categories bc, blog_categories_translations bct", @"b.id, bc.title category_name, COALESCE(b.description, bt.description) description,
                                bt.title, b.text, b.sefname, b.image, b.publish_date, b.views, b.is_active, b.create_date, 
                                b.update_date, b.is_mainpage", pageNumber, pageSize, $"b.id=bt.blog_id and bc.id=bct.category_id and b.category_id = bc.id and bct.lang_id = bt.lang_id and bt.lang_id={lang_id}", "b.publish_date desc");

                return ret;
            }
            catch
            {
                throw;
            }
        }

        public override BlogsDTO? getItem(int id, int lang_id)
        {
            try
            {
                var item = getItem("blogs b, blogs_translations bt", @"b.id, b.category_id, bt.title, b.title, b.sefname, b.image, b.publish_date, b.views,b.is_active, b.create_date, 
                                   b.update_date, b.is_mainpage", $"b.id = bt.blog_id and b.id={id} and bt.lang_id={lang_id}");

                return item;
            }
            catch
            {
                throw;
            }
        }

        public override void update(int id, IDto dto, int lang_id)
        {
            var blogs = dto as BlogsDTO;

            if (blogs == null)
                return;

            try
            {

                update<BlogsDTO>(id, "blogs", blogs, "title,text,category_id,sefname,publish_date,description,image,is_active,is_usefull,is_mainpage", lang_id);

                var blogsTranslationsDTO = new BlogsTranslationsDTO() { blog_id = id, lang_id = lang_id, title = blogs.title ?? "", description = blogs.description ?? "", text = blogs.text ?? "" };

                update<BlogsTranslationsDTO>("blogs_translations", $" blog_id ={id} and lang_id={lang_id}", blogsTranslationsDTO, "");
            }
            catch
            {
                throw;
            }
        }

        public override void create(BlogsDTO dto, int lang_id)
        {
            if (dto == null)
                return;

            var blogs = dto as BlogsDTO;


            var idContent = create<BlogsDTO>("blogs", dto, "id,category_id,title,text,sefname,,publish_date,description,is_active,is_usefull,is_mainpage", "id");


            BlogsTranslationsDTO blogsDTO = new BlogsTranslationsDTO()
            {
                blog_id = Convert.ToInt32(idContent["id"]),
                lang_id = lang_id,
                text = blogs.text ?? "",
                title = blogs.title ?? "",
                description = blogs.description ?? "",
            };

            create<BlogsTranslationsDTO>("blogs_translations", blogsDTO, "", "blog_id,lang_id");

        }

        public override bool delete(int id)
        {
            try
            {

                if (id < 0)
                    return false;

                if (delete("blogs_translations", $"blog_id={id}"))
                {
                    return delete("blogs", $"id={id}");
                }

                return false;

            }
            catch
            {
                throw;
            }

        }


        public override string BuildFilterConditions(string filter)
        {
            throw new NotImplementedException();
        }

    }
}
