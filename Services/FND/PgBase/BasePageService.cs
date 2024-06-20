using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Models.DTO.Interfaces;
using Models.FFIFND;
using Services.FND.Interfaces;
using Services.SQLCommandBuilder.Interfaces;
using Services.SQLCommandBuilder.PgSQLCommands;

namespace Services.FND.PgBase
{
    public abstract class BasePageService<T> : IBasePageService<T> where T : IDto, new()
    {
        protected ODDANP _odp;
        protected string error;
        protected string db_prefix = "db_nsk.";
        protected string _tableName;

        protected ISQLReadCommands _cmd_select;
        protected ISQLCreateCommands _cmd_insert;
        protected ISQLUpdateCommands _cmd_update;
        protected ISQLDeleteCommands _cmd_delete;

        public BasePageService(string tableName, IServiceProvider serviceProvider)
        {
            _odp = serviceProvider.GetService<ODDANP>() ?? throw new ArgumentNullException(nameof(ODDANP));
            _tableName = tableName ?? throw new ArgumentNullException(nameof(tableName));

            _cmd_select = serviceProvider.GetService<ISQLReadCommands>() ?? new CPgReadCommands();
            _cmd_insert = serviceProvider.GetService<ISQLCreateCommands>() ?? new CPgCreateCommands();
            _cmd_update = serviceProvider.GetService<ISQLUpdateCommands>() ?? new CPgUpdateCommands();
            _cmd_delete = serviceProvider.GetService<ISQLDeleteCommands>() ?? new CPgDeleteCommands();
        }

        public abstract IEnumerable<T> Index(int lang_id);

        public abstract void update(int id, IDto dto, int lang_id);

        public abstract void create(T dto, int lang_id);

        public abstract T? getItem(int id, int lang_id);

        public abstract bool delete(int id);


        protected IEnumerable<T> Index(string tableLst, string fields, string whereCond)
        {
            //string err = string.Empty;
            var sql = _cmd_select.BuildSelectCMD($"{db_prefix}{tableLst}", fields, whereCond);
            var lst = _odp.Routine.GetFromDatabase<T>(ref error, sql);

            if (!string.IsNullOrWhiteSpace(error))
                throw new Exception(error);

            return lst ?? null;
        }

        protected Dictionary<string, string>? create<T1>(string table, T1 dto, string excludeFields, string idFieldNames, string existCondition = "") where T1 : IDto
        {
            if (dto == null)
                return null;

            var idFieldsLst = idFieldNames.Split(',');
            string sql;
            Dictionary<string, string> ret;


            if (!string.IsNullOrWhiteSpace(existCondition))
            {
                sql = _cmd_select.BuildSelectCMD<T>($"{db_prefix}{table}", existCondition);

                var res1 = _odp.Routine.QueryAsDictionary(table, existCondition, ref error, idFieldNames);

                ret = res1.Where(t => idFieldsLst.Contains(t.Key)).ToDictionary<string, string>();

                if (ret.Count() > 0)
                {
                    string whereClause = string.Join(" and ", ret.Select(t => $"{t.Key}={t.Value}").ToList());
                    sql = _cmd_update.BuildUpdateCMD<T1>(table, whereClause, dto, excludeFields);

                    _odp.Routine.UpdateFromSql(sql, ref error);

                    if (!string.IsNullOrWhiteSpace(error))
                        throw new Exception(error);
                }

                return ret;

            }

            sql = _cmd_insert.BuildInsertCMD<T1>(table, dto, excludeFields);
            //sql = _cmd_insert.BuildInsertWithReturningIdCMD<T1>(table, dto, excludeFields, idFieldNames);

            ret = _odp.Routine.ExecReturningTransaction(new[] { sql }, idFieldsLst, ref error).Select(t => (t.Key, Convert.ToString(t.Value)??"")).ToDictionary<string, string>();



            if (!string.IsNullOrWhiteSpace(error))
                throw new Exception(error);

            return ret;
        }



        protected void update<T>(int id, string tableName, T dto, string exclude_fields, int lang_id) where T : IDto
        {
            try
            {
                if (dto == null)
                    return;

                string sql = _cmd_update.BuildUpdateCMD<T>(tableName, id, dto, exclude_fields);

                _odp.Routine.UpdateFromSql(sql, ref error);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new Exception(error);
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        protected void update<T>(string tableName, string whereClause, T dto, string exclude_fields) where T : IDto
        {
            try
            {
                if (dto == null)
                    return;

                string sql = _cmd_update.BuildUpdateCMD<T>(tableName, whereClause, dto, exclude_fields);

                _odp.Routine.UpdateFromSql(sql, ref error);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new Exception(error);
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        protected bool delete(string table, string whereClause)
        {

            string sql = _cmd_delete.BuildDeleteCMD(table, whereClause);

            if (!string.IsNullOrWhiteSpace(sql) && sql.Contains("Error: param"))
                throw new ArgumentException(sql);

            bool res = _odp.Routine.DeleteFromDB(table, whereClause, ref error);

            if (!string.IsNullOrWhiteSpace(error))
                throw new Exception(error);

            return res;

        }

        protected T? getItem(string tableLst, string fields, string whereCond)
        {

            var sql = _cmd_select.BuildSelectCMD($"{tableLst}", fields, $"{whereCond}");

            var items = _odp.Routine.GetFromDatabase<T>(ref error, sql);

            var item = items.FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(error))
                throw new Exception(error);

            if (item == null)
            {
                item = new T();

                item.SetDefaultValues();
            }

            return item;

        }

    }
}
