using Microsoft.AspNetCore.Identity;
using MovieReservationSystem.Dtos.ApplicationUserDtos;
using MovieReservationSystem.Entities;
using MovieReservationSystem.Services.Interfaces;

namespace MovieReservationSystem.Services.Repository
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<MovieReservationSystem.Entities.ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationUserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task DepositMoney(DepositMoneyDto depositMoneyDto)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            if (currentUser == null)
            {
                throw new Exception("Logged in user not found");
            }


            var user = await _userManager.GetUserAsync(currentUser);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Money += depositMoneyDto.Money;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("Error updating user money balance");
            }
        }

        public async Task UserLogin(LoginDto loginDto)
        {
            try
            {
                // Email ile kullanıcıyı bul
                var user = await _userManager.FindByEmailAsync(loginDto.Email);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

                if (!result.Succeeded)
                {
                    throw new Exception("Invalid login attempt");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Login failed: " + ex.Message);
            }
        }


        public async Task UserSignUp(SignUpDto signUpDto)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = signUpDto.Username,  // Kullanıcı adı
                    Email = signUpDto.Email,       // Kullanıcı e-posta
                    Name = signUpDto.Name,         // Kullanıcı adı
                    Surname = signUpDto.Surname,   // Kullanıcı soyadı
                    Money = 0                       // Başlangıç parası
                };

                // Kullanıcıyı veritabanına ekle
                var result = await _userManager.CreateAsync(user, signUpDto.Password);
            }
            catch (Exception ex)
            {
                throw new Exception("Signup failed");
            }

        }

        public async Task UserUpdate(UpdateDto updateDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(updateDto.Email);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                user.Name = updateDto.Name ?? user.Name;  
                user.Surname = updateDto.Surname ?? user.Surname;
                user.Email = updateDto.Email ?? user.Email;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    throw new Exception("User update failed");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Update failed: " + ex.Message);
            }
        }


        public async Task WithdrawMoney(WithdrawMoneyDto withdrawMoneyDto)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            if (currentUser == null)
            {
                throw new Exception("Logged in user not found");
            }

            var user = await _userManager.GetUserAsync(currentUser);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Money += withdrawMoneyDto.Money;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("Error updating user money balance");
            }
        }
    }
}
