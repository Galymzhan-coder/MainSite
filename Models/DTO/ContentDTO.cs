using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Models.DTO
{
    public class ContentDTO:IDto
    {
        public int Id { get; set; }
        public string? title { get; set; }
        public string? text { get; set; }
        public string? sefname { get; set; }
        public bool is_active { get; set; }
        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }
        public string? meta_description { get; set; }
        public string? meta_keywords { get; set; }
        public string? meta_image { get; set; }
        public string? meta_title { get; set; }
    }
}
