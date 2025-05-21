using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.Dtos.HallDtos;
using MovieReservationSystem.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MovieReservationSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly IHallService _hallService;

        public HallController(IHallService hallService)
        {
            _hallService = hallService;
        }

        [HttpPost("createhall")]
        public async Task<IActionResult> CreateHall( CreateHallDto createHallDto)
        {
            try
            {
                await _hallService.CreateHall(createHallDto);
                return Ok("Hall created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("deletehall")]
        public async Task<IActionResult> DeleteHall(int id)
        {
            try
            {
                await _hallService.DeleteHall(id);
                return Ok("Hall deactivated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getbyidhall")]
        public async Task<IActionResult> GetByIdHall(int id)
        {
            try
            {
                var result = await _hallService.GetByIdHall(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("updatehall")]
        public async Task<IActionResult> UpdateHall( UpdateHallDto updateHallDto)
        {
            try
            {
                await _hallService.UpdateHall(updateHallDto);
                return Ok("Hall updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getallhalls")]
        public async Task<IActionResult> GetAllHalls()
        {
            try
            {
                var halls = await _hallService.GetAllHalls();
                return Ok(halls);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}