using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieReservationSystem.Dtos.TicketDtos;
using MovieReservationSystem.Services.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieReservationSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "User", Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("buyticket")]
        public async Task<IActionResult> BuyTicket([FromBody] BuyTicketDto buyTicketDto)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized("User is not authenticated.");
                }

                int userId = int.Parse(userIdClaim.Value);

                await _ticketService.BuyTicket(userId, buyTicketDto);
                return Ok("Tickets purchased successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}