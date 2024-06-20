using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mysqlx.Notice.Warning.Types;
using static System.Net.Mime.MediaTypeNames;

namespace Models.DTO
{
    public class PagesTranslationsDTO: IDto
    {
        public int page_id { get; set; }
        public int lang_id { get; set; }
        public string title { get; set; }
        public string short_text { get; set; }
        public string text { get; set; }
        public string meta_title { get; set; }
        public string meta_description { get; set; }
        public string meta_keywords { get; set; }

        public void SetDefaultValues()
        {
            page_id = 0;

            lang_id = 1;

            title = string.Empty;

            text = string.Empty;

            meta_title = string.Empty;

            meta_description = string.Empty;

            meta_keywords = string.Empty;

        }
    }
}
