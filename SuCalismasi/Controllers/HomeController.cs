#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Water.Models;

namespace Water.Controllers
{
    public class HomeController : Controller
    {
        private readonly WaterContext _context;

        public HomeController(WaterContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var waterContext = _context.Products.Include(p => p.Brand).Include(p => p.Material);
            return View(await waterContext.ToListAsync());
        }
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Material)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        } 
    }
}
