using Models.DTO.Interfaces;
using Services.SQLCommandBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SQLCommandBuilder.PgSQLCommands
{
    public class CPgUpdateCommands: ISQLUpdateCommands
    {
        public string BuildUpdateCMD<T>(string tableName, int id, T dto, string excludeFields = "") where T : IDto
        {
            var sql = new StringBuilder($"UPDATE {tableName} SET ");
            var properties = typeof(T).GetProperties();
            var parameters = new List<string>();

            var arr_exclude = excludeFields.Replace(" ","").Split(',');

            foreach (var property in properties)
            {
                // Пропускаем свойства, которые указаны в списке исключений
                if (arr_exclude?.Contains(property.Name) == true)
                    continue;

                var value = property.GetValue(dto);
                var formattedValue = FormatSqlValue(value);
                parameters.Add($"{property.Name} = {formattedValue}");
            }

            sql.Append(string.Join(", ", parameters));
            sql.Append($" WHERE id = {id}");

            return sql.ToString();
        }

        public string BuildUpdateCMD<T>(string tableName, string whereClause, T dto, string excludeFields = "") where T : IDto
        {
            var sql = new StringBuilder($"UPDATE {tableName} SET ");
            var properties = typeof(T).GetProperties();
            var parameters = new List<string>();

            var arr_exclude = excludeFields.Replace(" ", "").Split(',');

            foreach (var property in properties)
            {
                // Пропускаем свойства, которые указаны в списке исключений
                if (arr_exclude?.Contains(property.Name) == true)
                    continue;

                var value = property.GetValue(dto);
                var formattedValue = FormatSqlValue(value);
                parameters.Add($"{property.Name} = {formattedValue}");
            }

            sql.Append(string.Join(", ", parameters));
            sql.Append($"WHERE {whereClause}");

            return sql.ToString();
        }

        private string FormatSqlValue(object value)
        {
            // Безопасное форматирование значений для SQL-запроса, предотвращение инъекций
            if (value == null)
                return "NULL";
            if (value is string || value is DateTime)
                return $"'{value.ToString().Replace("'", "''")}'"; // Очень простая обработка для предотвращения инъекций
            if (value is bool)
                return (bool)value ? "true" : "false";
            return value.ToString();
        }
    }
}
