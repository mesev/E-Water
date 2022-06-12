#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Water.Models;

namespace Water.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly WaterContext _context;

        public ProductsController(WaterContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var waterContext = _context.Products.Include(p => p.Brand).Include(p => p.Material);
            return View(await waterContext.ToListAsync());
        }
        public Product Product(short id)
        {

            return _context.Products.Include(m=>m.Brand).Include(m=>m.Material).FirstOrDefault(m=>m.ProductId == id);
        }

        // GET: Admin/Products/Details/5
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

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName");
            ViewData["MaterialId"] = new SelectList(_context.Materials, "MaterialId", "MaterialName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Volume,Price,BrandId,MaterialId,Image")] Product product)
        {
            string fileName;
            FileStream imageStream;
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                fileName = Directory.GetCurrentDirectory() + "\\wwwroot\\Images\\" + product.ProductId.ToString() + Path.GetExtension(product.Image.FileName);
                imageStream = new FileStream(fileName, FileMode.Create);
                await product.Image.CopyToAsync(imageStream);
                imageStream.Close();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", product.BrandId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "MaterialId", "MaterialName", product.MaterialId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", product.BrandId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "MaterialId", "MaterialName", product.MaterialId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("ProductId,Volume,Price,BrandId,MaterialId,Image")] Product product)
        {
            string fileName;
            FileStream imageStream;

            if (id != product.ProductId)
            {
                return NotFound();
            }
           

            if (ModelState.IsValid)
            {
                
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    if (product.Image != null)
                    {
                        fileName = Directory.GetCurrentDirectory() + "\\wwwroot\\Images\\" + product.ProductId.ToString() + Path.GetExtension(product.Image.FileName);
                        imageStream = new FileStream(fileName, FileMode.OpenOrCreate);
                        await product.Image.CopyToAsync(imageStream);
                        imageStream.Close();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", product.BrandId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "MaterialId", "MaterialName", product.MaterialId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(short? id)
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

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(short id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
