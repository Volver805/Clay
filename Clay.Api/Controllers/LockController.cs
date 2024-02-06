using Clay.Api.Requests;
using Clay.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clay.Api.Controllers
{
    [ApiController]
    [Route("api/locks")]
    public class LockController : Controller
    {
        private ILockService _lockService;
        public LockController(ILockService lockService)
        {
            _lockService = lockService;
        }
    
        [HttpPost("unlock/{lockId}")]
        [Authorize]
        public async Task<IActionResult> Unlock(int lockId)
        {
            try
            {
                HttpContext.Items.TryGetValue("UserId", out var userId);
                await _lockService.UpdateLockStatus(lockId, false, (int) userId);
                return Ok(new { Message = "Door unlocked successfully" });
            } catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Server Error", Message = "Something Went Wrong." });
            }
        }

        [HttpPost("lock/{lockId}")]
        [Authorize]
        public async Task<IActionResult> Lock(int lockId)
        {
            try
            {
                HttpContext.Items.TryGetValue("UserId", out var userId);
                await _lockService.UpdateLockStatus(lockId, true, (int) userId);
                return Ok(new { Message = "Door locked successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Server Error", Message = "Something Went Wrong." });
            }
        }
    }
}
