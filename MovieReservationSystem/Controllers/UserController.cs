using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.Dtos.ApplicationUserDtos;
using MovieReservationSystem.Entities;
using MovieReservationSystem.Services.Interfaces;

namespace MovieReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUserService _userService;

        public UserController(IApplicationUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> CreateAccount(SignUpDto signUpDto)
        {
            try
            {
                await _userService.UserSignUp(signUpDto);
                return CreatedAtAction(nameof(CreateAccount), new { message = "User created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while creating the user: {ex.Message}" });
            }
        }
        [HttpPost("DepositMoney")]
        public async Task<IActionResult> DepositMoney(DepositMoneyDto depositMoneyDto)
        {
            try
            {
                await _userService.DepositMoney(depositMoneyDto);
                return CreatedAtAction(nameof(DepositMoney), new { message = "User deposit money successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deposit money {ex.Message}" });
            }
        }
        [HttpPost("WithdrawMoney")]
        public async Task<IActionResult> WithdrawMoney(WithdrawMoneyDto withdrawMoneyDto)
        {
            try
            {
                await _userService.WithdrawMoney(withdrawMoneyDto);
                return CreatedAtAction(nameof(withdrawMoneyDto), new { message = "User deposit money successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deposit money {ex.Message}" });
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin(LoginDto loginDto)
        {
            try
            {
                await _userService.UserLogin(loginDto);
                return Ok(new { message = "Login successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Login failed: {ex.Message}" });
            }
        }

    }
}
