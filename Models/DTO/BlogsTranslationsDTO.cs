using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTO.Interfaces;

namespace Models.DTO
{
    public class BlogsTranslationsDTO: IDto
    {
        public int blog_id { get; set; }
        public int lang_id { get; set; }
        public string title { get; set; } = null;
        public string description { get; set; } = null;
        public string text { get; set; } = null;

        public void SetDefaultValues()
        {
            blog_id = 0;

            lang_id = 1;

            title = string.Empty;

            description = string.Empty;

            text = string.Empty;
        }
    }
}
