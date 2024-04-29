using Models.DTO;
using Models.FFIFND;
using Services.FND.Interfaces;
using Services.Utils;
using Models.DTO.Interfaces;

namespace Services.FND
{
    public abstract class DBService<T> : IDBService<T> where T : IDto, new()
    {
        private ODDANP _odp;
        private string error ;
        private string db_prefix = "db_nsk.";
        private string _tableName;

        public DBService(ODDANP odp, string tableName) 
        { 
            _odp = odp;
            _tableName = tableName;
        }

        public abstract IEnumerable<T> Index();

        public abstract List<T> getHierarchyLst();

        public abstract T? getItem(int id);

        protected IEnumerable<T> Index(string whereCond) 
        {
            string err = string.Empty;
            var sql = SqlCommandBuilder.BuildSelectCommand<T>($"{db_prefix}{_tableName}", whereCond);
            var lst = _odp.Routine.GetFromDatabase<T>(ref err, sql);
            Console.WriteLine(lst);
            return lst??null ;
        }

        public void create()
        {

        }


        public void update(int id, T dto) 
        {
            /*
            var category = dto as CategoryDTO;
            if (category == null)
                return;
            */
            if (dto == null)
                return;

            string sql = SqlCommandBuilder.BuildUpdateCommand<T>(_tableName, id, dto);

            _odp.Routine.UpdateFromSql( sql, ref error);
        }

        public void delete(int id)
        {

        }

        public List<T> getHierarchyLst(string whereCond) 
        {

            string err = string.Empty;

            //var sql = SqlCommandBuilder.BuildSelectCommand<T>($"{db_prefix}{_tableName}", "is_active=1");
            var sql = SqlCommandBuilder.BuildSelectCommand<T>($"{db_prefix}{_tableName}", whereCond);

            var lst = _odp.Routine.GetFromDatabase<T>(ref err, sql);

            return lst.ToList();

        }

        public T? getItem(int id, string whereCond)
        {

            string err = string.Empty;

            //var sql = SqlCommandBuilder.BuildSelectCommand<T>($"{db_prefix}{_tableName}", $"is_active=1 and id={id}");
            var sql = SqlCommandBuilder.BuildSelectCommand<T>($"{db_prefix}{_tableName}", $"id={id} {(string.IsNullOrEmpty(whereCond)?"":$"and {whereCond}") }");

            var item = _odp.Routine.GetFromDatabase<T>(ref err, sql).FirstOrDefault();

            return item;

        }
       /*
        public T? getItem(int id, string whereCond)
        {
             string err = string.Empty;

            //var sql = SqlCommandBuilder.BuildSelectCommand<T>($"{db_prefix}categories", $"is_active=1 and id={id}");
            var sql = SqlCommandBuilder.BuildSelectCommand<T>($"{db_prefix}categories", whereCond);

            var item = _odp.Routine.GetFromDatabase<T>(ref err, sql).FirstOrDefault();

            return item;

        }
       */
    }
}
