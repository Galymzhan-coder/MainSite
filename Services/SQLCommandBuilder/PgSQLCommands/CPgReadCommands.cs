using Models.DTO.Interfaces;
using Mysqlx.Crud;
using Services.SQLCommandBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SQLCommandBuilder.PgSQLCommands
{
    public class CPgReadCommands : ISQLReadCommands
    {
        public string BuildSelectCMD<T>(string tables, string whereConditions = "") where T : IDto
        {
            if (string.IsNullOrWhiteSpace(tables))
                return "Error: param \"Table List\" is empty!";

            var properties = typeof(T).GetProperties()
                .Where(prop => prop.CanRead)
                .Select(prop => prop.Name);

            var fields = string.Join(", ", properties);            

            var sql = $"SELECT {fields} FROM {tables}";

            if (!string.IsNullOrEmpty(whereConditions))
            {
                sql += $" WHERE {whereConditions}";
            }

            return sql;
        }

        public string BuildSelectCMD(string table, string fields, string whereConditions = "")
        {
            if (string.IsNullOrWhiteSpace(fields))
                return "Error: param \" fields\" is empty!";


            var sql = $"SELECT {fields} FROM {table}";

            if (!string.IsNullOrEmpty(whereConditions))
            {
                sql += $" WHERE {whereConditions}";
            }

            return sql;
        }

        public string BuildSelectPaginatedCMD(string tables, string fields, int page_num, int page_size, string whereConditions = "", string orderBy = "")
        {
            if (string.IsNullOrWhiteSpace(fields))
                return "Error: param \" fields\" is empty!";

            string where = string.IsNullOrEmpty(whereConditions) ? "" : $" where {whereConditions}";
            string order = string.IsNullOrEmpty(orderBy) ? "" : $" order by {orderBy}";

            var sql = @$" with sorted_tab as (select row_number() over(order by(select 1)) as row_num, {fields} from {tables} {where} {order})
                          select * from sorted_tab where row_num > {(page_num-1)* page_size} and row_num <= {page_num*page_size}";
            
            return sql;
        }
    }
}
