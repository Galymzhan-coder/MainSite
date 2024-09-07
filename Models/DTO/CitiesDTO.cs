using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public partial class CitiesDTO: IDto
    {
        public int id { get; set; }
        public string? title { get; set; }
        public bool is_active { get; set; }
        public int latitude { get; set; }
        public int longitude { get; set; }
        public string? phone { get; set; }
        public string? phone_secondary { get; set; }
        public string? geoip_city { get; set; }
        public DateTime create_date { get; set; }
        public DateTime update_date { get; set;}

        public void SetDefaultValues()
        {
            id = 0;
            title = string.Empty;
            is_active = false;
            latitude = 0; 
            longitude = 0; 
            phone = string.Empty;
            phone_secondary = string.Empty;
            geoip_city = string.Empty;
            create_date = DateTime.MinValue;
            update_date = DateTime.MinValue;
        }
    }
}
