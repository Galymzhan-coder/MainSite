using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Models.DTO
{
    public class PagesDTO:IDto
    {
        public int Id { get; set; }

        public int user_id { get; set; }

        public int category_id { get; set; }

        public string? title { get; set; }

        public string? short_text { get; set; }

        public string? text { get; set; }

        public string? sefname { get; set; }

        public string? image { get; set; }

        public DateTime publish_date { get; set; }

        public int views { get; set; }

        public bool is_active { get; set; }

        public DateTime create_date { get; set; }

        public DateTime update_date { get; set; }

        public string? meta_description { get; set; }

        public string? meta_keywords { get; set; }

        public string? meta_image { get; set; }

        public string? meta_title { get; set; }

        public bool is_mainpage { get; set; }

        public string? download_file { get; set; }

        public void SetDefaultValues()
        {
            Id = 0;

            user_id = 0;

            category_id = 0;

            title = string.Empty;

            short_text = string.Empty;

            text = string.Empty;

            sefname = string.Empty;

            image = string.Empty;

            publish_date = DateTime.MinValue;

            views = 0;

            is_active = false;

            create_date = DateTime.MinValue;

            update_date = DateTime.MinValue;

            meta_description = string.Empty;

            meta_keywords = string.Empty;

            meta_image = string.Empty;

            meta_title = string.Empty;

            is_mainpage = false;

            download_file = string.Empty;
        }
    }
}
