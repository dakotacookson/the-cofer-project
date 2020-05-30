using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class favStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public favStatusController(ApplicationDbContext ctx,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }
        // GET: favStatus
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,CodeId,UserId,code")] favStatus favStatus, Code codeid)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                favStatus.UserId = user.Id;
                _context.Add(favStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(favStatus);
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var UserCode = _context.favStatus.Include(m => m.code)
            .Where(m => m.UserId == user.Id);
            return View(UserCode);
        }

        public async Task<IActionResult> Delete(int? id , Code codes)
        {
            var user = await GetCurrentUserAsync();
            //if (user.Id != codes.UserId)
            //{
            //    return NotFound();
            //}

            var favStatus = await _context.favStatus
                .Include(f => f.code)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favStatus == null)
            {
                return NotFound();
            }

            return View(favStatus);
        }

        // POST: favStatus1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favStatus = await _context.favStatus.FindAsync(id);
            _context.favStatus.Remove(favStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool favStatusExists(int id)
        {
            return _context.favStatus.Any(e => e.Id == id);
        }
    }
}
