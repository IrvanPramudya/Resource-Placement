using API.DTOs.Accounts;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize]
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
        [Authorize]
        public IActionResult EmployeeIndex() => View();
        [Authorize(Roles = "Admin,Operasional")]
        public IActionResult ClientIndex() => View();
        [Authorize(Roles = "Admin,Operasional")]
        public IActionResult InterviewIndex() => View();
        [Authorize(Roles = "Admin,Trainer,Operasional")]
        public IActionResult GradeIndex() => View();
        [Authorize(Roles = "Admin,Operasional")]
        public IActionResult PositionIndex() => View();
        [Authorize(Roles = "Admin,Operasional")]
        public IActionResult PlacementIndex() => View();
        [Authorize(Roles = "Admin")]
        public IActionResult AccountIndex() => View();
        [Authorize(Roles = "Admin")]
        public IActionResult RoleIndex() => View();
        [Authorize(Roles = "Admin")]
        public IActionResult AccountRoleIndex() => View();
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult NotifikasiIndex() => View();
        [Authorize]
        public IActionResult DetailIndex() => View();
        [Authorize(Roles = "Admin,Operasional")]
        public IActionResult ResultIndex() => View();
        [AllowAnonymous]
        public IActionResult ChangePassword() => View();
    }
}
