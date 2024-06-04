using Models.DTO.Interfaces;

namespace Services.SQLCommandBuilder.Interfaces
{
    public interface ISQLUpdateCommands
    {
        string BuildUpdateCMD<T>(string tableName, int id, T dto, string excludeFields = null) where T : IDto;

        string BuildUpdateCMD<T>(string tableName, string whereClause, T dto, string excludeFields = null) where T : IDto;
    }
}
