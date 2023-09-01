using API.DTOs.Accounts;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class GradeController : Controller
    {
        public IActionResult EmployeeGraded() => View();
        public IActionResult EmployeeUngraded() => View();
    }
}
