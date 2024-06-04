using Models.DTO.Interfaces;

namespace Models.DTO;

public partial class AdminLogDTO: IDto
{
    public int id { get; set; }

    public int user_id { get; set; }

    public string module { get; set; } = null!;

    public string action { get; set; } = null!;

    public int item_id { get; set; }

    public string? message { get; set; }

    public DateTime create_date { get; set; }
    
    public void SetDefaultValues()
    {
        id = 0;

        user_id = 0;

        module = string.Empty;

        action = string.Empty;

        item_id = 0;

        message = string.Empty;

        create_date = DateTime.MinValue;

    }
}
