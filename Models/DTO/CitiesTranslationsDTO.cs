using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public partial class CitiesTranslationsDTO : IDto
    {
        public int city_id { get; set; }
        public int lang_id { get; set; }
        public string? title { get; set; } 

        public void SetDefaultValues()
        {
            city_id = 0;
            lang_id = 0;
            title = string.Empty;
        }
    }
}
