using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.Services.Interfaces;

namespace MovieReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatsService _seatsService;

        public SeatController(ISeatsService seatsService)
        {
            _seatsService = seatsService;
        }
        [HttpGet("GetByScreeningIdAvaibleSeats")]
        public async Task<IActionResult> GetByScreeningIdAvaibleSeats(int id)
        {
            try
            {
                var result = await _seatsService.GetByScreeningIdAvaibleSeats(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
