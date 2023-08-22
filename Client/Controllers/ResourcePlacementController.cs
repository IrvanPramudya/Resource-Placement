using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ResourcePlacementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult EmployeeIndex() => View();
        public IActionResult ClientIndex() => View();
        public IActionResult GradeIndex() => View();
        public IActionResult PositionIndex() => View();
        public IActionResult PlacementIndex() => View();
        public IActionResult AccountIndex() => View();
    }
}
