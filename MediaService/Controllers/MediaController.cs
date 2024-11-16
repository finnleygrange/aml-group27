using Microsoft.AspNetCore.Mvc;

namespace MediaService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : Controller
    {
        [HttpGet]
        public IActionResult GetMedia()
        {
            return Ok(new { Message = "Hello from MediaService!" });
        }
    }

}
