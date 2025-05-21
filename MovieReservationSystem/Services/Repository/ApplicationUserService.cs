using Microsoft.AspNetCore.Identity;
using MovieReservationSystem.Dtos.ApplicationUserDtos;
using MovieReservationSystem.Entities;
using MovieReservationSystem.Services.Interfaces;
using MovieReservationSystem.Services.Token;

namespace MovieReservationSystem.Services.Repository
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<MovieReservationSystem.Entities.ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;

        public ApplicationUserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
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

        public async Task<LoginUserResponseDto> UserLogin(LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var role = roles.FirstOrDefault();
                    Dtos.JwtDtos.Token token = _tokenService.CreateAccessTokenForUser(user, role, 5);

                    return new LoginUserSuccessResponseDto()
                    {
                        Token = token,
                    };
                }
                return new LoginUserErrorResponseDto()
                {
                    Message = "Kullanıcı adı veya şifre hatalıdır"
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Login failed: " + ex.Message);
            }
        }

        public async Task<LoginAdminResponseDto> AdminLogin(LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var role = roles.FirstOrDefault();
                    Dtos.JwtDtos.Token token = _tokenService.CreateAccessTokenForAdmin(user, role, 5);

                    return new LoginAdminSuccessResponseDto()
                    {
                        Token = token,
                    };
                }
                return new LoginAdminErrorResponseDto()
                {
                    Message = "Kullanıcı adı veya şifre hatalıdır"
                };
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
                    UserName = signUpDto.Username,
                    Email = signUpDto.Email,
                    Name = signUpDto.Name,
                    Surname = signUpDto.Surname,
                    Money = 0
                };

                var result = await _userManager.CreateAsync(user, signUpDto.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                else
                {
                    throw new Exception("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Signup failed" + ex.Message);
            }
        }

        public async Task AdminSignUp(SignUpDto signUpDto)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = signUpDto.Username,
                    Email = signUpDto.Email,
                    Name = signUpDto.Name,
                    Surname = signUpDto.Surname,
                    Money = 0
                };

                var result = await _userManager.CreateAsync(user, signUpDto.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    throw new Exception("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Signup failed" + ex.Message);
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

        public async Task AddRolesToExistingUser(ApplicationUser user)
        {
            // Gerekli rolleri veritabanına ekle
            await SeedRoles();

            // Kullanıcıya rol ata
            await AssignRoleToUser(user, "User");
        }

        private async Task SeedRoles()
        {
            var roleManager = _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<RoleManager<ApplicationUserRole>>();

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new ApplicationUserRole { Name = "User" });
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new ApplicationUserRole { Name = "Admin" });
            }
        }

        private async Task AssignRoleToUser(ApplicationUser user, string role)
        {
            if (!await _userManager.IsInRoleAsync(user, role))
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }


    }
}
