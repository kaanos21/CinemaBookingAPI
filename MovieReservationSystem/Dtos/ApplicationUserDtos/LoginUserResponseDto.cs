using MovieReservationSystem.Dtos.JwtDtos;

namespace MovieReservationSystem.Dtos.ApplicationUserDtos
{
    public class LoginUserResponseDto
    {
        
    }

    public class LoginUserSuccessResponseDto: LoginUserResponseDto
    {
        public Token Token { get; set; }
    }
    public class LoginUserErrorResponseDto:LoginUserResponseDto
    {
        public string Message {  get; set; }
    }
}
