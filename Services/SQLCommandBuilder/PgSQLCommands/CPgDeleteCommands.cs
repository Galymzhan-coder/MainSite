using Services.SQLCommandBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SQLCommandBuilder.PgSQLCommands
{
    public class CPgDeleteCommands : ISQLDeleteCommands
    {
        public string BuildDeleteCMD(string table, int id, string whereConditions = "")
        {
            if (string.IsNullOrWhiteSpace(table))
                return "Error: param \" table\" is empty!";

            if (id<0)
                return "Error: param \" id\" = 0!";

            var sql = $"DELETE FROM {table} where id={id}";

            if (!string.IsNullOrEmpty(whereConditions))
            {
                sql += $" OR {whereConditions}";
            }

            return sql;
        }

        public string BuildDeleteCMD(string table, string whereConditions)
        {
            if (string.IsNullOrWhiteSpace(table))
                return "Error: param \" table\" is empty!";

            if (string.IsNullOrEmpty(whereConditions))
                return "Error: param \" whereConditions\" is empty!";

            var sql = $"DELETE FROM {table} where {whereConditions}";            

            return sql;
        }
    }
}
