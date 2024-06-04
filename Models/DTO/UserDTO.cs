using Models.DTO.Interfaces;

namespace Models.DTO;

public partial class UserDTO : IDto
{
    public int id { get; set; }

    public string username { get; set; } = null!;

    public string password { get; set; } = null!;

    public string email { get; set; } = null!;

    public string? first_name { get; set; }

    public string? last_name { get; set; }

    public string? role { get; set; }

    public bool is_active { get; set; }

    public DateTime create_date { get; set; }

    public DateTime update_date { get; set; }

    public string phone { get; set; } = null!;

    public int? sms_code { get; set; }

    public string iin { get; set; } = null!;

    public string? doc_num { get; set; }

    public short? is_connected { get; set; }

    public string? onesignal_id { get; set; }

    public int? sendinblue_id { get; set; }

    public int? nsk_client_id { get; set; }


    public void SetDefaultValues()
    {
        id = 0;

        username = string.Empty;

        password = string.Empty;

        email = string.Empty;

        first_name = string.Empty;

        last_name = string.Empty;

        role = string.Empty;

        is_active = false;

        create_date = DateTime.MinValue;

        update_date = DateTime.MinValue;

        phone = string.Empty;

        sms_code = 0;

        iin = string.Empty;

        doc_num = string.Empty;

        onesignal_id = string.Empty;

        sendinblue_id = 0;

        nsk_client_id = 0;

    }
}
