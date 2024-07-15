﻿using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SQLCommandBuilder.Interfaces
{
    public interface ISQLReadCommands
    {
        string BuildSelectCMD<T>(string tables, string whereConditions = "") where T : IDto;


        string BuildSelectCMD(string tables, string fields, string whereConditions = "");

        string BuildSelectPaginatedCMD(string tables, string fields, int page_num, int page_size, string whereConditions = "", string orderBy = "");

    }
}
