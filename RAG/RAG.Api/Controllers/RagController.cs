using Microsoft.AspNetCore.Mvc;

namespace RAG.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RagController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
