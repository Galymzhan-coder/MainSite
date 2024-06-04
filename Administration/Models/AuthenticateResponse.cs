using Models.DTO;

namespace Administration.Models
{
    public class AuthenticateResponse
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public AuthenticateResponse(UserDTO user, string token)
        {
            FirstName = user.first_name;
            LastName = user.last_name;
            Token = token;
        }
    }
}
