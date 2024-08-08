using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public partial class TopManagementTranslationsDTO : IDto
    {
        public int manager_id { get; set; }

        public int lang_id { get; set; }

        public string? title { get; set; }

        public string full_name { get; set; } 

        public string? text { get; set; }
     


        public void SetDefaultValues()
        {
            manager_id = 0;

            lang_id = 1;

            title = string.Empty;

            full_name = string.Empty;

            text = string.Empty;
            
        }
    }
}
