using Microsoft.AspNetCore.Mvc;

namespace shoppingApp.Controllers
{
    public class ordersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
