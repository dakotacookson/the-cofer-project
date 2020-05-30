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
    public class CodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public CodesController(ApplicationDbContext ctx,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [Authorize]
        public ViewResult IndexTitleSearch(string searchString)
        {
            var Codes = _context.Code.Where(p => p.Hiden == false);
            if (!string.IsNullOrEmpty(searchString))
            {
                 Codes = _context.Code.Where(p => p.Hiden == false && p.Title.Contains(searchString) || p.Hiden == false && p.CodeLanguge.Contains(searchString));
            }

            else
            {
                Codes = _context.Code.Where(p => p.Hiden == false);
            }
            return View(Codes);
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {

            var user = await GetCurrentUserAsync();
            if (user == null )
            {
                return NotFound();
            }
            var Code = _context.Code
            .Where(m => m.Hiden == false);

            return View(Code);
        }


        [Authorize]
        public async Task<IActionResult> MyCode()
        {
            var user = await GetCurrentUserAsync();
            if ( user.Id == null)
            {
                return NotFound();
            }
            var UserCode = _context.Code
                .Where(m => m.UserId == user.Id);
            return View(UserCode);
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            var user = await GetCurrentUserAsync();
            if (id == null)
            {
                return NotFound();
            }

            var code = await _context.Code
                .FirstOrDefaultAsync(m => m.Id == id);
            if (code == null)
            {
                return NotFound();
            }
            if (code.Hiden  == true && code.UserId != user.Id)
            {
                return NotFound();
            }

            return View(code);
        }

        [Authorize]
        public async Task<IActionResult> MyDetails(int? id)
        {
            var user = await GetCurrentUserAsync();
            if (id == null)
            {
                return NotFound();
            }

            var code = await _context.Code
                .FirstOrDefaultAsync(m => m.Id == id);

            if (code == null || user.Id != code.UserId)
            {
                return NotFound();
            }


            return View(code);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,CodeLanguge,Descreption,CodeDeposit,Date,UserId,Hiden")] Code code)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                code.UserId = user.Id;
                code.date = DateTime.Now;
                _context.Add(code);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MyCode));
            }
            return View(code);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await GetCurrentUserAsync();
            if (id == null)
            {
                return NotFound();
            }

            var code = await _context.Code.FindAsync(id);
            if (code == null || user.Id != code.UserId)
            {
                return NotFound();
            }
            return View(code);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CodeLanguge,Descreption,CodeDeposit,Date,UserId,Hiden")] Code codes)
        {
            var user = await GetCurrentUserAsync();
                if (id != codes.Id)
            {
                return NotFound();
            }
            codes.UserId = user.Id;
            codes.date = DateTime.Now;
            _context.Add(codes);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(codes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CodeExists(codes.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MyCode));
            }
            return View(codes);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await GetCurrentUserAsync();
            if (user.Id == null)
            {
                return NotFound();
            }

            var code = await _context.Code
                .FirstOrDefaultAsync(m => m.Id == id);
            if (code == null || user.Id != code.UserId)
            {
                return NotFound();
            }

            return View(code);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var code = await _context.Code.FindAsync(id);
            _context.Code.Remove(code);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyCode));
        }

        private bool CodeExists(int id)
        {
            return _context.Code.Any(e => e.Id == id);
        }
    }
}
