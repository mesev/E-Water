#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Areas.Admin.Water.Models;
using System.Security.Cryptography;
using System.Text;
using System;
namespace Water.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly AdminContext _context;

        public UsersController(AdminContext context)
        {
            _context = context;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            var adminContext = _context.Users.Include(u => u.Branch);
            return View(await adminContext.ToListAsync());
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Branch)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "BranchId");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,Password,ConfirmPassword,Email,Authorization,BranchId")] User user)
        {
            SHA256 sHA256;
            byte[] hashedPassword, userPassword;

            if (ModelState.IsValid)
            {
                sHA256 = SHA256.Create();
                userPassword=Encoding.Unicode.GetBytes(user.UserName + user.Password);
                hashedPassword=sHA256.ComputeHash(userPassword);
                user.Password = BitConverter.ToString(hashedPassword).Replace("-", "");
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "BranchId", user.BranchId);
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "BranchId", user.BranchId);
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("UserId,UserName,Password,ConfirmPassword,Email,Authorization,BranchId")] User user,string OldPassword ,string OriginalPassword)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }
            SHA256 sHA256;
            byte[] hashedPassword, userPassword;
            string oldHash;
            if (ModelState.IsValid)
            {
                sHA256 = SHA256.Create();
                userPassword = Encoding.Unicode.GetBytes(user.UserName.Trim() + OldPassword);
                hashedPassword = sHA256.ComputeHash(userPassword);
                oldHash = BitConverter.ToString(hashedPassword).Replace("-", "");
                if (oldHash == OriginalPassword)
                {
                    userPassword = Encoding.Unicode.GetBytes(user.UserName + user.Password);
                    hashedPassword = sHA256.ComputeHash(userPassword);
                    user.Password = BitConverter.ToString(hashedPassword).Replace("-", "");
                    try
                    {
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(user.UserId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "BranchId", user.BranchId);
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Branch)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(short id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
