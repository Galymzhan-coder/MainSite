using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class ContentTranslationsDTO : IDto
    {
        public int content_id { get; set; }
        public int lang_id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string meta_title { get; set; }
        public string meta_description {  get; set; }
        public string meta_keywords { get; set; }

    }
}
