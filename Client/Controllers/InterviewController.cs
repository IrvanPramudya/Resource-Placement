using API.DTOs.Accounts;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize(Roles = "Admin,Operasional")]
    public class InterviewController : Controller
    {
        public IActionResult EmployeeAccepted() => View();
        public IActionResult EmployeeRejected() => View();
        public IActionResult ClientAccepted() => View();
        public IActionResult ClientRejected() => View();
        public IActionResult EmployeeWaiting() => View();
    }
}
