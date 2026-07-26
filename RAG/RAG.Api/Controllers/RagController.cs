using Microsoft.AspNetCore.Mvc;

namespace RAG.Api.Controllers
{
    public class RagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
