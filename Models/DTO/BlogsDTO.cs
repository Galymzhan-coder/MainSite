using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTO.Interfaces;

namespace Models.DTO
{
    public partial class BlogsDTO : IDto
    {
        public int id {  get; set; }
        public int? category_id {  get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? text { get; set; }
        public DateTime publish_date { get; set; }
        public string? sefname { get; set; }
        public int? views { get; set; }
        public bool is_active { get; set; }
        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }
        public string? image { get; set; }
        public bool is_usefull { get; set; }
        public bool is_mainpage { get; set; }

        public void SetDefaultValues()
        {
            id            = 0;
            category_id   = 0;
            title         = string.Empty;
            description   = string.Empty;
            text          = string.Empty;
            publish_date  = DateTime.MinValue;
            sefname       = string.Empty;
            views         = 0;
            is_active     = false;
            create_date   = DateTime.MinValue;
            update_date   = DateTime.MinValue;
            image         = string.Empty;
            is_usefull    = false;
            is_mainpage   = false;
        }


    }
}
