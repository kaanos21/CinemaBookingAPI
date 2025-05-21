using MovieReservationSystem.Entities;

namespace MovieReservationSystem.Services.Token
{
    public interface ITokenService
    {
        Dtos.JwtDtos.Token CreateAccessTokenForUser(ApplicationUser user, string role, int minute);
        Dtos.JwtDtos.Token CreateAccessTokenForAdmin(ApplicationUser user, string role, int minute);
    }
}
