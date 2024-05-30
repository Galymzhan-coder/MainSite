using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models.DTO.Interfaces;
using Services.SQLCommandBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SQLCommandBuilder.PgSQLCommands
{
    public class CPgCreateCommands : ISQLCreateCommands
    {
        public string BuildInsertCMD<T>(string table, T dto) where T : IDto// NOT READY
        {
            var sql = new StringBuilder($"Insert into {table} ");
            var properties = typeof(T).GetProperties();
            var parameters = new List<string>();
            var values = new List<string>();

            foreach (var property in properties)
            {
                var value = property.GetValue(dto);
                var formattedValue = FormatSqlValue(value);
                parameters.Add($"{property.Name}");
                values.Add($"{formattedValue}");
            }

            sql.Append($"({string.Join(", ", parameters)}) ");
            sql.Append($" values({string.Join(", ", values)}) ");
            //sql.Append($" WHERE id = {id}");

            return sql.ToString();
        }

        public string BuildInsertCMD<T>(string table, T dto, string excludeFields) where T : IDto// NOT READY
        {
            var sql = new StringBuilder($"Insert into {table} ");
            var properties = typeof(T).GetProperties();
            var parameters = new List<string>();
            var values = new List<string>();

            var arr_exclude = excludeFields.Replace(" ", "").Split(',');

            foreach (var property in properties)
            {
                // Пропускаем свойства, которые указаны в списке исключений
                if (arr_exclude?.Contains(property.Name) == true)
                    continue;
                var value = property.GetValue(dto);
                var formattedValue = FormatSqlValue(value);
                parameters.Add($"{property.Name}");
                values.Add($"{formattedValue}");
            }

            sql.Append($"({string.Join(", ", parameters)}) ");
            sql.Append($" values({string.Join(", ", values)}) ");
            //sql.Append($" WHERE id = {id}");

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
                return (bool)value ? "1" : "0";
            return value.ToString();
        }
    }
}
