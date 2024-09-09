using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class ProductsDTO:IDto
    {
        public int id { get; set; }
        public int category_id { get; set; }
        public string? title { get; set; }        
        public string? sefname { get; set; }
        public string? short_description { get; set; }
        public string? description { get; set; }
        public string? small_image { get; set; }
        public string? image { get; set; }
        public bool is_active { get; set; }
        public uint? position { get; set; }
        public uint? is_large { get; set; }
        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }
        public uint? calculator_id { get; set; }
        public string? text { get; set; }
        public string? suggested_label { get; set; }
        public string? suggested_button_label { get; set; }
        public uint? is_plain_page { get; set; }
        public bool has_banner { get; set; }
        public string? banner_title { get; set; }
        public string? banner_text { get; set; }
        public string? banner_image { get; set; }
        public bool has_description { get; set; }
        public string? meta_description { get; set; }
        public string? meta_keywords { get; set; }
        public string? meta_image { get; set; }
        public string? meta_title { get; set; }

        public void SetDefaultValues()
        {
            id = 0;
            category_id = 0;
            title = string.Empty;
            sefname = string.Empty;
            short_description = string.Empty;
            description = string.Empty;
            small_image = string.Empty;
            image = string.Empty;
            is_active = false;
            position = null;
            is_large = null;
            create_date = DateTime.MinValue;
            update_date = DateTime.MinValue;
            calculator_id = null;
            text = string.Empty;
            suggested_label = string.Empty;
            suggested_button_label = string.Empty;
            is_plain_page = null;
            has_banner = false;
            banner_title = string.Empty;
            banner_text = string.Empty;
            banner_image = string.Empty;
            has_description = false;
            meta_description = string.Empty;
            meta_keywords = string.Empty;
            meta_image = string.Empty;
            meta_title = string.Empty;
        }

    }
}
