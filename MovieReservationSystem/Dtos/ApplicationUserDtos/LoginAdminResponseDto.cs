using MovieReservationSystem.Dtos.JwtDtos;

namespace MovieReservationSystem.Dtos.ApplicationUserDtos
{
    public class LoginAdminResponseDto
    {
    }

    public class LoginAdminSuccessResponseDto : LoginAdminResponseDto
    {
        public Token Token { get; set; }
    }
    public class LoginAdminErrorResponseDto : LoginAdminResponseDto
    {
        public string Message { get; set; }
    }
}
