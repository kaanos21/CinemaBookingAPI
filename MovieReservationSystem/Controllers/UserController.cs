using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("UserSignUp")]
        public async Task<IActionResult> CreateAccountForUser(SignUpDto signUpDto)
        {
            try
            {
                await _userService.UserSignUp(signUpDto);
                return Ok("Create Account For User Succesfull");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while creating the user: {ex.Message}" });
            }
        }
        [HttpPost("AdminSignUp")]
        public async Task<IActionResult> CreateAccountForAdmin(SignUpDto signUpDto)
        {
            try
            {
                await _userService.AdminSignUp(signUpDto);
                return Ok("Create Account For Admin Succesfull");
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

        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin(LoginDto loginDto)
        {
            try
            {
                var response = await _userService.UserLogin(loginDto);

                if (response is LoginUserSuccessResponseDto successResponse)
                {
                    return Ok(successResponse); // Token bilgisi burada dönecek
                }
                else if (response is LoginUserErrorResponseDto errorResponse)
                {
                    return BadRequest(errorResponse);
                }

                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Login failed: {ex.Message}" });
            }
        }
        [HttpPost("AdminLogin")]
        public async Task<IActionResult> AdminLogin(LoginDto loginDto)
        {
            try
            {
                var response = await _userService.AdminLogin(loginDto);

                if (response is LoginAdminSuccessResponseDto successResponse)
                {
                    return Ok(successResponse); // Token bilgisi burada dönecek
                }
                else if (response is LoginAdminErrorResponseDto errorResponse)
                {
                    return BadRequest(errorResponse);
                }

                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Login failed: {ex.Message}" });
            }
        }

    }
}
