using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public partial class BlogCategoriesDTO : IDto
    {
        public int id { get; set; }
        public string? title { get; set; }
        public string? sefname { get; set; }
        public bool is_active { get; set; } 
        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }
        public BlogCategoriesDTO()
        {
            // Устанавливаем текущую дату при создании
            create_date = DateTime.Now;
            update_date = DateTime.Now;
        }
        public void SetDefaultValues()
        {
            id = 0;
            title = string.Empty;
            sefname = string.Empty;
            is_active = false;
            update_date = DateTime.Now;
        }
    }
}
