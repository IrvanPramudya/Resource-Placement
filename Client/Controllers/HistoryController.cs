using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        public IActionResult EmployeeHistory() => View();
    }
}
