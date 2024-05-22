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
            if (!string.IsNullOrWhiteSpace(table))
                return "Error: param \" table\" is empty!";


            var sql = $"DELETE FROM {table} where id={id}";

            if (!string.IsNullOrEmpty(whereConditions))
            {
                sql += $" OR {whereConditions}";
            }

            return sql;
        }
    }
}
