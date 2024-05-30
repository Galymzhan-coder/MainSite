using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Models.DTO;
using Models.DTO.Interfaces;
using Models.FFIFND;
using MySqlX.XDevAPI.Common;
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

        protected IEnumerable<T> Index(string tableLst, string fields, string whereCond)
        {
            //string err = string.Empty;
            var sql = _cmd_select.BuildSelectCMD($"{db_prefix}{tableLst}", fields, whereCond);
            var lst = _odp.Routine.GetFromDatabase<T>(ref error, sql);
            
            if(!string.IsNullOrWhiteSpace(error))
                throw new Exception(error);

            return lst ?? null;
        }

        public abstract void create(T dto, int lang_id);

        public Dictionary<string,string> create<T1>(string table, T1 dto, string excludeFields, string idFieldNames, string existCondition = "") where T1 : IDto
        {
            if (dto == null)
                return null;

            var idFieldsLst = idFieldNames.Split(',');
            string sql;
            Dictionary<string, string> ret;


            if (!string.IsNullOrWhiteSpace(existCondition))
            {
                sql = _cmd_select.BuildSelectCMD<T>($"{db_prefix}{table}", existCondition);

                //string tblname, string whereclause, ref string message, string listfield = "*"
                var res1 = _odp.Routine.QueryAsDictionary(table, existCondition, ref error, idFieldNames);// GetFromDatabase<CategoryDTO>(ref error, sql).FirstOrDefault();

                ret = res1.Where(t=> idFieldsLst.Contains(t.Key)).ToDictionary<string,string>();

                if(ret.Count() > 0)
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

            ret = _odp.Routine.ExecReturningTransaction(new [] {sql}, idFieldsLst, ref error ).Select(t=>(t.Key,t.Value.ToString())).ToDictionary<string,string>();

            

            if (!string.IsNullOrWhiteSpace(error))
                throw new Exception(error);

            return ret;
        }

        public abstract void update(int id, IDto dto, int lang_id);

        protected void update<T>(int id,string tableName, T dto, string exclude_fields, int lang_id) where T : IDto
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
                throw(new Exception(ex.Message));
            }
        }

        protected void update<T>(string tableName,string whereClause, T dto, string exclude_fields) where T : IDto
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

        public void delete(int id)
        {

        }

        public abstract T? getItem(int id, int lang_id);

        protected T? getItem(string tableLst, string fields, string whereCond)
        {

            var sql = _cmd_select.BuildSelectCMD($"{tableLst}", fields, $"{whereCond}");

            var item = _odp.Routine.GetFromDatabase<T>(ref error, sql).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(error))
                throw new Exception(error);

            return item;

        }

    }
}
