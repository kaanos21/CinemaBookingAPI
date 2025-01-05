using MovieReservationSystem.Dtos.ApplicationUserDtos;



namespace MovieReservationSystem.Services.Interfaces
{
    public interface IApplicationUserService
    {
        Task UserLogin(LoginDto loginDto);
        Task UserSignUp(SignUpDto signUpDto);
        Task UserUpdate(UpdateDto updateDto);
        Task DepositMoney(DepositMoneyDto depositMoneyDto);
        Task WithdrawMoney(WithdrawMoneyDto withdrawMoneyDto);
    }
}
