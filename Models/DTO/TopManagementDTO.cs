using Models.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public partial class TopManagementDTO : IDto
    {
        public int Id { get; set; }

        public string? title { get; set; }

        public string full_name { get; set; } = null!;

        public string? text { get; set; }

        public string? image { get; set; }

        public string? image_description { get; set; }

        public uint? position { get; set; }

        public uint? weight { get; set; }

        public bool is_active { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }
       


        public void SetDefaultValues()
        {
            Id = 0;

            title = string.Empty;

            full_name = string.Empty;

            text = string.Empty;

            image = string.Empty;

            image_description = string.Empty;

            position = 0;

            weight = 0;

            is_active = false;

            created_at = DateTime.MinValue;

            updated_at = DateTime.MinValue;
            
        }
    }
}
