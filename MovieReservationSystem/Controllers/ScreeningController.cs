using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.Dtos.ScreeningDtos;
using MovieReservationSystem.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MovieReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningController : ControllerBase
    {
        private readonly IScreeningService _screeningService;

        public ScreeningController(IScreeningService screeningService)
        {
            _screeningService = screeningService;
        }

        [HttpPost("createscreening")]
        public async Task<IActionResult> CreateScreening(CreateScreeningDto createScreeningDto)
        {
            try
            {
                await _screeningService.CreateScreening(createScreeningDto);
                return Ok("Screening created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("deletescreening")]
        public async Task<IActionResult> DeleteScreening(int id)
        {
            try
            {
                await _screeningService.DeleteScreening(id);
                return Ok("Screening deleted successfully.");
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

        [HttpGet("getbyidscreening")]
        public async Task<IActionResult> GetByIdScreening(int id)
        {
            try
            {
                var result = await _screeningService.GetByIdScreening(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("updatescreening")]
        public async Task<IActionResult> UpdateScreening(UpdateScreeningDto updateScreeningDto)
        {
            try
            {
                await _screeningService.UpdateScreening(updateScreeningDto);
                return Ok("Screening updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getallscreenings")]
        public async Task<IActionResult> GetAllScreenings()
        {
            try
            {
                var screenings = await _screeningService.GetAllScreenings();
                return Ok(screenings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getbyidscreeningdetail")]
        public async Task<IActionResult> GetByIdScreeningDetail(int id)
        {
            try
            {
                var result = await _screeningService.GetByIdScreeningDetail(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getlistscreeningbymovideıd")]
        public async Task<IActionResult> GetByMovieIdScreenings(int id)
        {
            try
            {
                var result = await _screeningService.GetByMovieIdScreenings(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getbyhallıdscreenings")]
        public async Task<IActionResult> GetByHallIdScreenings(int id)
        {
            try
            {
                var result = await _screeningService.GetByHallIdScreenings(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}