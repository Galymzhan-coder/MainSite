using Models.DTO.Interfaces;

namespace Models.Entities;

public partial class ContactsTranslationDTO: IDto
{
    public int ContactId { get; set; }

    public int LangId { get; set; }

    public string? Title { get; set; }

    public string? Address { get; set; }

    public string? WorkHours { get; set; }

    public string? Director { get; set; }


    public void SetDefaultValues()
    {
        ContactId = 0;
        LangId = 1;
        Title = null; 
        Address = null; 
        WorkHours = null; 
        Director = null;        
    }
}
