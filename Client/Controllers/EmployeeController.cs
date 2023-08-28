using API.DTOs.Accounts;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult EmployeeSite() => View();
        public IActionResult EmployeeIdle() => View();
    }
}
