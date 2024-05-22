using Microsoft.Extensions.DependencyInjection;
using Models.DTO.Interfaces;
using Models.FFIFND;
using Services.FND.Interfaces;
using Services.SQLCommandBuilder.Interfaces;
using Services.SQLCommandBuilder.PgSQLCommands;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FND.PgBase
{
    public abstract class BasePageService<T> : IBasePageService<T> where T : IDto, new()
    {
        private ODDANP _odp;
        private string error;
        private string db_prefix = "db_nsk.";
        private string _tableName;

        ISQLReadCommands _cmd_select;
        ISQLCreateCommands _cmd_insert;
        ISQLUpdateCommands _cmd_update;
        ISQLDeleteCommands _cmd_delete;
        /*
        public BasePageService(string tableName)
        {
            _tableName = tableName;
        }
        /*
        public BasePageService(ODDANP odp, string tableName, ISQLReadCommands cmd_select, ISQLCreateCommands cmd_insert, ISQLUpdateCommands cmd_update, ISQLDeleteCommands cmd_delete)
        {
            _odp = odp;
            _tableName = tableName;

            _cmd_select = cmd_select;
            _cmd_insert = cmd_insert;
            _cmd_update = cmd_update;
            _cmd_delete = cmd_delete;
        }
        */
        public BasePageService(string tableName, IServiceProvider serviceProvider)
        {
            _odp = serviceProvider.GetService<ODDANP>();
            _tableName = tableName;
            _cmd_select = serviceProvider.GetService<ISQLReadCommands>() ?? new CPgReadCommands();
            _cmd_insert = serviceProvider.GetService<ISQLCreateCommands>() ?? new CPgCreateCommands();
            _cmd_update = serviceProvider.GetService<ISQLUpdateCommands>() ?? new CPgUpdateCommands();
            _cmd_delete = serviceProvider.GetService<ISQLDeleteCommands>() ?? new CPgDeleteCommands();
        }

        public abstract IEnumerable<T> Index(int lang_id);

        protected IEnumerable<T> Index(string tableLst, string fields, string whereCond)
        {
            string err = string.Empty;
            var sql = _cmd_select.BuildSelectCMD($"{db_prefix}{tableLst}", fields, whereCond);
            var lst = _odp.Routine.GetFromDatabase<T>(ref err, sql);
            
            return lst ?? null;
        }

        public void create()
        {

        }

        public abstract void update(int id, IDto dto, int lang_id);

        protected void update<T>(int id,string tableName, T dto, string exclude_fields, int lang_id) where T : IDto
        {
            try
            {
                if (dto == null)
                    return;

                //string sql = SqlCommandBuilder.BuildUpdateCommand<T>(_tableName, id, dto, null);
                string sql = _cmd_update.BuildUpdateCMD<T>(tableName, id, dto, exclude_fields);

                _odp.Routine.UpdateFromSql(sql, ref error);
            }
            catch (Exception ex) 
            { 

            }
        }

        protected void update(string tableName,string whereClause, IDto dto, string exclude_fields)
        {
            try
            {
                if (dto == null)
                    return;

                //string sql = SqlCommandBuilder.BuildUpdateCommand<T>(_tableName, id, dto, null);
                //string sql = _cmd_update.BuildUpdateCMD(tableName, id, dto, exclude_fields);
                string sql = _cmd_update.BuildUpdateCMD(tableName, whereClause, dto, exclude_fields);

                _odp.Routine.UpdateFromSql(sql, ref error);
            }
            catch (Exception ex)
            {

            }
        }

        public void delete(int id)
        {

        }

        public abstract T? getItem(int id, int lang_id);

        //public T? getItem(int id, string whereCond)
        protected T? getItem(string tableLst, string fields, string whereCond)
        {

            string err = string.Empty;
            //var sql = SqlCommandBuilder.BuildSelectCommand<T>($"{db_prefix}{_tableName}", $"id={id} {(string.IsNullOrEmpty(whereCond) ? "" : $"and {whereCond}")}");
            var sql = _cmd_select.BuildSelectCMD($"{tableLst}", fields, $"{whereCond}");

            var item = _odp.Routine.GetFromDatabase<T>(ref err, sql).FirstOrDefault();

            return item;

        }

    }
}
