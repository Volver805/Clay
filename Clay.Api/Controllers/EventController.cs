using Clay.Api.Requests;
using Clay.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clay.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("history")]
        [Authorize]
        public async Task<IActionResult> ListEventHistory([FromQuery] ListEventHistoryRequest request)
        {
            try
            {
                var events = await _eventService.ListEvents(request.lockId, request.userId);
                return Ok(new { Message = "List of Events retrieved successfully", Events = events });
            } catch(Exception ex)
            {
                return StatusCode(500, new { Error = "Server Error", Message = "Something Went Wrong." });
            }
        }
    }
}
