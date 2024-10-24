using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab4.Data;
using Lab4.Models;
using Lab4.Infrastructure.Filters;

namespace Lab4.Controllers
{
    [TypeFilter(typeof(TimingLogAttribute))] // Фильтр ресурсов
    [ExceptionFilter] // Фильтр исключений
    public class PacksController : Controller
    {
        private readonly Db8011Context _context;

        public PacksController(Db8011Context context)
        {
            _context = context;
        }

        // GET: Packs
        public async Task<IActionResult> Index()
        {
            var db8011Context = _context.Packs.Include(p => p.User);
            return View(await db8011Context.ToListAsync());
        }

        // GET: Packs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pack = await _context.Packs
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PackId == id);
            if (pack == null)
            {
                return NotFound();
            }

            return View(pack);
        }

        // GET: Packs/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Packs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PackId,PackName,UserId")] Pack pack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", pack.UserId);
            return View(pack);
        }

        // GET: Packs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pack = await _context.Packs.FindAsync(id);
            if (pack == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", pack.UserId);
            return View(pack);
        }

        // POST: Packs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PackId,PackName,UserId")] Pack pack)
        {
            if (id != pack.PackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackExists(pack.PackId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", pack.UserId);
            return View(pack);
        }

        // GET: Packs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pack = await _context.Packs
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PackId == id);
            if (pack == null)
            {
                return NotFound();
            }

            return View(pack);
        }

        // POST: Packs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pack = await _context.Packs.FindAsync(id);
            if (pack != null)
            {
                _context.Packs.Remove(pack);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackExists(int id)
        {
            return _context.Packs.Any(e => e.PackId == id);
        }
    }
}
