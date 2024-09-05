using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public partial class ContactsDTO : IDto
    {
        public int id {  get; set; }
        public int city_id { get; set; }
        public string? title { get; set; } 
        public string? phones { get; set; }
        public string? email { get; set; } 
        public string? address { get; set; }
        public string? longitude { get; set; }
        public string? latitude { get; set; }
        public string? work_hours { get; set; }
        public string? director { get; set; } 
        public bool is_active { get; set; }
        public bool is_payment_center { get; set; }
        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }

        public void SetDefaultValues()
        {
            id = 0;
            city_id = 0;
            title = string.Empty;
            phones = string.Empty;
            email = string.Empty;
            address = string.Empty;
            longitude = string.Empty;
            latitude = string.Empty;
            work_hours = string.Empty;
            director = string.Empty;
            is_active = false;
            is_payment_center = false;
            create_date = DateTime.MinValue;
            update_date = DateTime.MinValue;
        }
    }
}
