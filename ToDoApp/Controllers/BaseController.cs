using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Controllers
{
    public class BaseController : Controller
    {
        public virtual IActionResult Index()
        {
            return View();
        }
    }
}
