using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class CategoriesTranslationsDTO : IDto
    {
        public int category_id { get; set; }

        public int lang_id { get; set; }

        public string? title { get; set; }

        public string? description { get; set; }

        public void SetDefaultValues()
        {
            category_id = 0;

            lang_id = 1;

            title = string.Empty;

            description = string.Empty;
        }
    }
}
