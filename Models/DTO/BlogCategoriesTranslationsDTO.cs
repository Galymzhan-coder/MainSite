using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public partial class BlogCategoriesTranslationsDTO : IDto
    {
        public int category_id { get; set; }
        public int lang_id { get; set; }
        public string title { get; set; } = null;
        public void SetDefaultValues()
        {
            category_id = 0;
            lang_id = 1;
            title = string.Empty;
        }
    }

}
