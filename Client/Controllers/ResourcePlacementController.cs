﻿using Microsoft.AspNetCore.Mvc;

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
        public IActionResult RoleIndex() => View();
        public IActionResult AccountRoleIndex() => View();
    }
}
