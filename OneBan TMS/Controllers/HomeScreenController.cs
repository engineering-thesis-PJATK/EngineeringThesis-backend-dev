using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OneBan_TMS.Controllers
{
    public class HomeScreenController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return new BadRequestResult();
        }
    }
}