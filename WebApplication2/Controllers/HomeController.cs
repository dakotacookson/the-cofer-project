using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ApplicationDbContext ctx,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }

        public async Task<IActionResult> Index()
        {
            {
                var list = (from p in _context.Code
                            orderby p.date descending
                            where p.Hiden == false
                            select p).Take(5);
                return View(await list.ToListAsync());
            }
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
