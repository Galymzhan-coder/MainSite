using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SQLCommandBuilder.Interfaces
{
    public interface ISQLCreateCommands
    {
        string BuildInsertCMD<T>(string table, T dto) where T : IDto;

        string BuildInsertCMD<T>(string table, T dto, string excludeFields) where T : IDto;

        string BuildInsertWithReturningIdCMD<T>(string table, T dto, string excludeFields, string returningFields) where T : IDto;
    }
}
