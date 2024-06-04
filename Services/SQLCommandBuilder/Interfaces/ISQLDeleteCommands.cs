using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SQLCommandBuilder.Interfaces
{
    public interface ISQLDeleteCommands
    {
        string BuildDeleteCMD(string table, int id, string whereConditions = "");

        string BuildDeleteCMD(string table, string whereConditions);
    }
}
