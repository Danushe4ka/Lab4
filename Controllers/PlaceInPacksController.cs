using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab4;
using Lab4.Data;
using Lab4.Infrastructure.Filters;

namespace Lab4.Controllers
{
    [TypeFilter(typeof(TimingLogAttribute))] // Фильтр ресурсов
    [ExceptionFilter] // Фильтр исключений
    public class PlaceInPacksController : Controller
    {
        private readonly Db8011Context _context;

        public PlaceInPacksController(Db8011Context context)
        {
            _context = context;
        }

        // GET: PlaceInPacks
        public async Task<IActionResult> Index()
        {
            var db8011Context = _context.PlaceInPacks.Include(p => p.Pack).Include(p => p.Place);
            return View(await db8011Context.ToListAsync());
        }

        // GET: PlaceInPacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeInPack = await _context.PlaceInPacks
                .Include(p => p.Pack)
                .Include(p => p.Place)
                .FirstOrDefaultAsync(m => m.PipId == id);
            if (placeInPack == null)
            {
                return NotFound();
            }

            return View(placeInPack);
        }

        // GET: PlaceInPacks/Create
        public IActionResult Create()
        {
            ViewData["PackId"] = new SelectList(_context.Packs, "PackId", "PackId");
            ViewData["PlaceId"] = new SelectList(_context.Places, "PlaceId", "PlaceId");
            return View();
        }

        // POST: PlaceInPacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PipId,PlaceId,PackId")] PlaceInPack placeInPack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(placeInPack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PackId"] = new SelectList(_context.Packs, "PackId", "PackId", placeInPack.PackId);
            ViewData["PlaceId"] = new SelectList(_context.Places, "PlaceId", "PlaceId", placeInPack.PlaceId);
            return View(placeInPack);
        }

        // GET: PlaceInPacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeInPack = await _context.PlaceInPacks.FindAsync(id);
            if (placeInPack == null)
            {
                return NotFound();
            }
            ViewData["PackId"] = new SelectList(_context.Packs, "PackId", "PackId", placeInPack.PackId);
            ViewData["PlaceId"] = new SelectList(_context.Places, "PlaceId", "PlaceId", placeInPack.PlaceId);
            return View(placeInPack);
        }

        // POST: PlaceInPacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PipId,PlaceId,PackId")] PlaceInPack placeInPack)
        {
            if (id != placeInPack.PipId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(placeInPack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceInPackExists(placeInPack.PipId))
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
            ViewData["PackId"] = new SelectList(_context.Packs, "PackId", "PackId", placeInPack.PackId);
            ViewData["PlaceId"] = new SelectList(_context.Places, "PlaceId", "PlaceId", placeInPack.PlaceId);
            return View(placeInPack);
        }

        // GET: PlaceInPacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeInPack = await _context.PlaceInPacks
                .Include(p => p.Pack)
                .Include(p => p.Place)
                .FirstOrDefaultAsync(m => m.PipId == id);
            if (placeInPack == null)
            {
                return NotFound();
            }

            return View(placeInPack);
        }

        // POST: PlaceInPacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var placeInPack = await _context.PlaceInPacks.FindAsync(id);
            if (placeInPack != null)
            {
                _context.PlaceInPacks.Remove(placeInPack);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceInPackExists(int id)
        {
            return _context.PlaceInPacks.Any(e => e.PipId == id);
        }
    }
}
