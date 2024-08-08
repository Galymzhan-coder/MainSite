using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class LanguagesDTO:IDto
    {
        public long id { get; set; }
        public string title { get; set; }
        public string code { get; set; }
        public string locale { get; set; }
        public bool is_active { get; set; }

        public void SetDefaultValues()
        { 
            id = 0;

            title = string.Empty;
            
            code = string.Empty;

            locale = string.Empty;

            is_active = false;
        }
    }
}
