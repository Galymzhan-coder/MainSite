using Models.DTO;
using Models.FFIFND;
using Services.FND.Interfaces;
using Services.Utils;
using Models.DTO.Interfaces;
using Services.FND.PgBase;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Services.FND
{
    //public class CategoriesService:ICategoriesService, DBService<CategoryDTO>//IBaseService<IDto>
    public class CategoriesService : BasePageExtIndexHierarchyService<CategoryDTO>, ICategoriesService //DBService<CategoryDTO>, ICategoriesService//, IBaseService<IDto>
    {
        //private ODDANP _odp;
        //private string error ;
        //private string db_prefix = "db_nsk.";

        public CategoriesService(IServiceProvider serviceProvider)
        : base("categories", serviceProvider)
        {
        }

        public override IEnumerable<CategoryDTO> IndexHierarchySorted(string indent_symbol, string indent_pre_symbol, int lang_id)
        {
            var lst = IndexHierarchySorted("categories",
                        $@"t.id, t.parent_id, COALESCE((select ct.title from categories_translations ct where t.id = ct.category_id and ct.lang_id={lang_id}), t.title) AS title, 
                              t.sefname, COALESCE((select ct.description from categories_translations ct where t.id = ct.category_id and ct.lang_id={lang_id}), t.description) AS description, 
                              t.is_active, t.create_date, t.update_date, t.root, t.lft, t.rgt",
                        "parent_id", "id", "title", indent_symbol, indent_pre_symbol, "");

            return lst;
        }
        /*
        public IEnumerable<IDto> Index(string tabName,string whereCond) 
        {
            string err = string.Empty;
            var sql = SqlCommandBuilder.BuildSelectCommand<CategoryDTO>($"{db_prefix}categories", " 1=1 order by root,lft,rgt");
            var lst = _odp.Routine.GetFromDatabase<CategoryDTO>(ref err, sql);

            return lst.ToList();
        }
        */
        /*
        public override IEnumerable<CategoryDTO> Index()
        {

            //var lst = Index($"{db_prefix}categories", " 1=1 order by root,lft,rgt");
            var lst = Index(" 1=1 order by root,lft,rgt");

            return lst;
        }
        */
        public override IEnumerable<CategoryDTO> Index(int lang_id)
        {
            try
            {
                //id, parent_id, title, sefname, description, is_active, create_date, update_date, root, lft, rgt,level
                //var lst = Index("categories c , categories_translations ct", "c.id, c.parent_id, COALESCE(ct.title , c.title) title, sefname, COALESCE(ct.description , c.description) description, is_active, create_date, update_date, root, lft, rgt,level", $"c.id =ct.category_id and ct.lang_id={lang_id}");

                /*var sql = _cmd_select.BuildSelectHierarchySortedCMD($"categories", 
                           $@"t.id, t.parent_id, COALESCE((select ct.title from categories_translations ct where t.id = ct.category_id and ct.lang_id={lang_id}), t.title) AS title, 
                              t.sefname, COALESCE((select ct.description from categories_translations ct where t.id = ct.category_id and ct.lang_id={lang_id}), t.description) AS description, 
                              t.is_active, t.create_date, t.update_date, t.root, t.lft, t.rgt", "parent_id", "id", "");
                
                var lst = _odp.Routine.GetFromDatabase<CategoryDTO>(ref error, sql);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new Exception(error);
                */

                var lst = IndexHierarchySorted("&nbsp;&nbsp;","", lang_id);
                return lst;
            }
            catch
            {
                throw;
            }
        }

        public override CategoryDTO? getItem(int id, int lang_id)
        {
            try
            {
                var item = getItem("categories c , categories_translations ct", "c.id, c.parent_id, COALESCE(ct.title , c.title) title, sefname, COALESCE(ct.description , c.description) description, is_active, create_date, update_date, root, lft, rgt,level", $"c.id =ct.category_id and c.id={id} and ct.lang_id={lang_id}");

                return item;
            }
            catch
            {
                throw;
            }

        }

        public override void update(int id, IDto dto, int lang_id)
        {
            var category = dto as CategoryDTO;

            if (category == null)
                return;

            try
            {

                update<CategoryDTO>(id, "categories", category, "id,title, description", lang_id);

                var categoriesTranslationsDTO = new CategoriesTranslationsDTO() { category_id = id, lang_id = lang_id, title = category.Title ?? "", description = category.Description ?? ""};

                update<CategoriesTranslationsDTO>("categories_translations", $" category_id ={id} and lang_id={lang_id}", categoriesTranslationsDTO, "");
            }
            catch
            {
                throw;
            }
        }

        public override void create(CategoryDTO dto, int lang_id)
        {
            if (dto == null)
                return;

            var category = dto as CategoryDTO;


            var idCategory = create<CategoryDTO>("categories", dto, "id,title, description", "id");


            CategoriesTranslationsDTO categoryTranslationsDTO = new CategoriesTranslationsDTO()
            {
                category_id = Convert.ToInt32(idCategory["id"]),
                lang_id = lang_id,
                title = category.Title ?? "",
                description = category.Description ?? ""
            };

            create<CategoriesTranslationsDTO>("categories_translations", categoryTranslationsDTO, "", "category_id,lang_id");

        }

        public override bool delete(int id)
        {
            try
            {

                if (id < 0)
                    return false;

                if (delete("categories_translations", $"category_id={id}"))
                {
                    return delete("categories", $"id={id}");
                }

                return false;

            }
            catch
            {
                throw;
            }

        }
        /*
        public  List<CategoryDTO> getHierarchyLst()
        {

            
            var lst = getHierarchyLst("is_active=1");

            return lst.ToList();

        }

        public override CategoryDTO? getItem(int id)
        {
            var item = getItem(id, "");
            return item;
        }
        */
        /*
        public void create()
        {

        }

        public void update(string tabName, int id, IDto dto) 
        {
            var category = dto as CategoryDTO;
            if (category == null)
                return;

            string sql = SqlCommandBuilder.BuildUpdateCommand<CategoryDTO>(tabName,id, category);

            _odp.Routine.UpdateFromSql( sql, ref error);
        }

        public void delete(int id)
        {

        }

        public List<CategoryDTO> getCategoryHierarchyLst() 
        {

            string err = string.Empty;

            var sql = SqlCommandBuilder.BuildSelectCommand<CategoryDTO>($"{db_prefix}categories", "");

            var lst = _odp.Routine.GetFromDatabase<CategoryDTO>(ref err, sql);

            return lst.ToList();

        }

        public CategoryDTO? getCategoryItem(int id)
        {

            string err = string.Empty;
 
            var sql = SqlCommandBuilder.BuildSelectCommand<CategoryDTO>($"{db_prefix}categories", $"id={id}");
            
            var item = _odp.Routine.GetFromDatabase<CategoryDTO>(ref err, sql).FirstOrDefault();

            return item;

        }
       
        public IDto? getItem(int id)
        {
             string err = string.Empty;

            var sql = SqlCommandBuilder.BuildSelectCommand<CategoryDTO>($"{db_prefix}categories", $"id={id}");

            var item = _odp.Routine.GetFromDatabase<CategoryDTO>(ref err, sql).FirstOrDefault();

            return item;

        }
        */

    }
}
