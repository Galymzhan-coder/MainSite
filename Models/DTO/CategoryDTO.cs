using Models.DTO.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace Models.DTO;

public partial class CategoryDTO:IDto
{
    public int Id { get; set; }

    public uint? parent_id { get; set; }

    public string? Title { get; set; }

    public string Sefname { get; set; } = null!;

    public string? Description { get; set; }

    public bool is_active { get; set; }

    public DateTime Create_Date { get; set; }

    public DateTime Update_Date { get; set; }

    public uint? Root { get; set; }

    public uint? Lft { get; set; }

    public uint? Rgt { get; set; }

    public uint? Level { get; set; }

    public void SetDefaultValues()
    {
        Id = 0;
        
        parent_id = 0;
        
        Title = string.Empty;  
        
        Sefname = string.Empty;  
        
        Description = string.Empty;

        is_active = false;

        Create_Date = DateTime.MinValue;

        Update_Date = DateTime.MinValue;

        Root = 0;

        Lft = 0;

        Rgt = 0;

        Level = 0;
    }

}
