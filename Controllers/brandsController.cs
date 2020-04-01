using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment1.Data;
using Assignment1.Models;

namespace Assignment1.Controllers
{
    public class brandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public brandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: brands
        public async Task<IActionResult> Index()
        {
            return View(await _context.brands.ToListAsync());
        }

        // GET: brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brands = await _context.brands
                .FirstOrDefaultAsync(m => m.brandId == id);
            if (brands == null)
            {
                return NotFound();
            }

            return View(brands);
        }

        // GET: brands/Create
        public IActionResult Create()
        {
            ViewData["brandId"] = new SelectList(_context.brands, "brandId", "brandId");
            return View();
        }

        // POST: brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("brandName,brandId,glassType")] brands brands)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brands);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["brandId"] = new SelectList(_context.brands, "brandId", "brandId", brands.brandId);
            return View(brands);
        }

        // GET: brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brands = await _context.brands.FindAsync(id);
            if (brands == null)
            {
                return NotFound();
            }
            ViewData["brandId"] = new SelectList(_context.brands, "brandId", "brandId", brands.brandId);
            return View(brands);
        }

        // POST: brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("brandName,brandId,glassType")] brands brands)
        {
            if (id != brands.brandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brands);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!brandsExists(brands.brandId))
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
            ViewData["brandId"] = new SelectList(_context.brands, "brandId", "brandId", brands.brandId);
            return View(brands);
        }

        // GET: brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brands = await _context.brands 
                .FirstOrDefaultAsync(m => m.brandId == id);
            if (brands == null)
            {
                return NotFound();
            }

            return View(brands);
        }

        // POST: brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brands = await _context.brands.FindAsync(id);
            _context.brands.Remove(brands);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool brandsExists(int id)
        {
            return _context.brands.Any(e => e.brandId == id);
        }
    }
}
