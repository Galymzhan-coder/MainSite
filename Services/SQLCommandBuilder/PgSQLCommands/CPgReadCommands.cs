using Models.DTO.Interfaces;
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

        public string BuildSelectCMD(string tables, string fields, string whereConditions = "")
        {
            if (string.IsNullOrWhiteSpace(fields))
                return "Error: param \" fields\" is empty!";


            var sql = $"SELECT {fields} FROM {tables}";

            if (!string.IsNullOrEmpty(whereConditions))
            {
                sql += $" WHERE {whereConditions}";
            }

            return sql;
        }
    }
}
