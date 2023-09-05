using API.DTOs.Accounts;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize(Roles = "Admin,Operasional")]
    public class ClientController : Controller
    {
        public IActionResult AvailableClient() => View();
        public IActionResult UnavailableClient() => View();
    }
}
