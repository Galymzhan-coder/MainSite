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

        public string BuildSelectHierarchySortedCMD(string table, string fields, string parent_id_col, string id_col, string indented_field, string indent_symbol, string indent_pre_symbol, string whereConditions = "")
        {
            if (string.IsNullOrWhiteSpace(table))
                return "Error: param \" table\" is empty!";

            if (string.IsNullOrWhiteSpace(fields))
                return "Error: param \" fields\" is empty!";

            if (string.IsNullOrWhiteSpace(parent_id_col))
                return "Error: param \" parent_id_col\" is empty!";

            if (string.IsNullOrWhiteSpace(id_col))
                return "Error: param \" id_col\" is empty!";
            /*
            var sql = $@"WITH RECURSIVE table_tree AS (
                            SELECT {(string.IsNullOrEmpty(fields) ? "t.*" : fields)}, {id_col} AS sort_id, 0 AS depth 
                            FROM {table} t WHERE {parent_id_col} IS NULL OR {parent_id_col} = 0
                            UNION ALL
                            SELECT {(string.IsNullOrEmpty(fields) ? "t.*" : fields)}, tt.sort_id, tt.depth + 1 
                            FROM {table} t  INNER JOIN table_tree tt ON tt.{id_col} = t.{parent_id_col}
                         )
                        SELECT t.*, {(string.IsNullOrEmpty(indent_symbol)? "": fields.Replace(indented_field, $"REPEAT({indent_symbol}, depth * 2){(indent_symbol==" "? "" : "|")} || {indented_field} AS {indented_field}")))} FROM table_tree t {(string.IsNullOrEmpty(whereConditions)? "": $" WHERE {whereConditions}")} ORDER BY sort_id, depth";            
            */
            var sql = $@"WITH RECURSIVE table_tree AS (
                            SELECT {(string.IsNullOrEmpty(fields) ? "t.*" : fields)}, {id_col} AS sort_id, 0 AS depth 
                            FROM {table} t WHERE {parent_id_col} IS NULL OR {parent_id_col} = 0
                            UNION ALL
                            SELECT {(string.IsNullOrEmpty(fields) ? "t.*" : fields)}, tt.sort_id, tt.depth + 1 
                            FROM {table} t  INNER JOIN table_tree tt ON tt.{id_col} = t.{parent_id_col}
                         )
                        SELECT t.*, {(string.IsNullOrEmpty(indent_symbol.Replace("'","")) ? "" : $"{(string.IsNullOrEmpty(indent_pre_symbol) ? "''" : $"'{indent_pre_symbol}'")} || REPEAT('{indent_symbol}', depth * 2) || {indented_field} AS indented_{indented_field}")} FROM table_tree t {(string.IsNullOrEmpty(whereConditions) ? "": $" WHERE { whereConditions}")} ORDER BY sort_id, depth";
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
