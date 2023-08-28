using API.DTOs.Accounts;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ResourcePlacementController : Controller
    {
        private readonly IAccountRepository repository;

        public ResourcePlacementController(IAccountRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var result = await repository.Login(login);
            if (result is null)
            {
                TempData["Error"] = $"Failed to Login! - {result.Message}!";
                return RedirectToAction("Login", "ResourcePlacement");
            }
            else if (result.Code == 409)
            {
                TempData["Error"] = $"Failed to Login! - {result.Message}!";
                ModelState.AddModelError(string.Empty, result.Message);
                return RedirectToAction("Login", "ResourcePlacement");
            }
            else if (result.Code == 200)
            {
                TempData["Success"] = $"Successfully Login!";
                HttpContext.Session.SetString("JWToken", result.Data.Token);
                return RedirectToAction("Index", "ResourcePlacement");
            }
            return View();
        }

        public IActionResult EmployeeIndex() => View();
        public IActionResult ClientIndex() => View();
        public IActionResult InterviewIndex() => View();
        public IActionResult GradeIndex() => View();
        public IActionResult PositionIndex() => View();
        public IActionResult PlacementIndex() => View();
        public IActionResult AccountIndex() => View();
        public IActionResult RoleIndex() => View();
        public IActionResult AccountRoleIndex() => View();
        public IActionResult NotifikasiIndex() => View();
        public IActionResult DetailIndex() => View();
        public IActionResult ResultIndex() => View();
    }
}
