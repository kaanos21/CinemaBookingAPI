using MovieReservationSystem.Dtos.ApplicationUserDtos;
namespace MovieReservationSystem.Services.Interfaces
{
    public interface IApplicationUserService
    {
        Task<LoginUserResponseDto> UserLogin(LoginDto loginDto);
        Task<LoginAdminResponseDto> AdminLogin(LoginDto loginDto);
        Task UserSignUp(SignUpDto signUpDto);
        Task AdminSignUp(SignUpDto signUpDto);
        Task UserUpdate(UpdateDto updateDto);
        Task DepositMoney(DepositMoneyDto depositMoneyDto);
        Task WithdrawMoney(WithdrawMoneyDto withdrawMoneyDto);
    }
}
