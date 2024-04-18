using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utils
{
    public class SqlCommandBuilder
    {
        public static string BuildUpdateCommand<T>(string tabName,int id, T dto) where T : IDto
        {
            /*
            string tabName = typeof(T).Name;
            if (tabName.EndsWith("DTO"))
                tabName = tabName[..^3];
            */
            var sql = new StringBuilder($"UPDATE {tabName} SET ");

            var properties = typeof(T).GetProperties();
            var parameters = new List<string>();
            foreach (var property in properties)
            {
                // Получаем значение свойства для текущего объекта dto
                var value = property.GetValue(dto);
                var formattedValue = FormatSqlValue(value);
                parameters.Add($"{property.Name} = {formattedValue}");
            }

            sql.Append(string.Join(", ", parameters));
            sql.Append($" WHERE id = {id}");

            return sql.ToString();
        }

        private static string FormatSqlValue(object value)
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

        /*
        public static string BuildUpdateCommand<T>(int id, T dto) where T : IDto
        {
            string tabName = typeof(T).Name;

            if (tabName.EndsWith("DTO"))
                tabName = tabName[..^3];

            var sql = new StringBuilder($"UPDATE {tabName} SET ");

            var properties = typeof(T).GetProperties();

            var parameters = new List<string>();
            foreach (var property in properties)
            {
                parameters.Add($"{property.Name} = @{property.Name}");
            }

            sql.Append(string.Join(", ", parameters));

            sql.Append(" WHERE id = @id");


            return sql.ToString();
        }
        */
        public static string BuildSelectCommand<T>(string tableName, string whereConditions = "") where T : IDto
        {
            var properties = typeof(T).GetProperties()
                .Where(prop => prop.CanRead)
                .Select(prop => prop.Name);

            var fields = string.Join(", ", properties);
            var sql = $"SELECT {fields} FROM {tableName}";

            if (!string.IsNullOrEmpty(whereConditions))
            {
                sql += $" WHERE {whereConditions}";
            }

            return sql;
        }
        
    }
}
