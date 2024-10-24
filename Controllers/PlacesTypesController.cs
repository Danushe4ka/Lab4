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
    public class PlacesTypesController : Controller
    {
        private readonly Db8011Context _context;

        public PlacesTypesController(Db8011Context context)
        {
            _context = context;
        }

        // GET: PlacesTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlacesTypes.ToListAsync());
        }

        // GET: PlacesTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placesType = await _context.PlacesTypes
                .FirstOrDefaultAsync(m => m.TypeId == id);
            if (placesType == null)
            {
                return NotFound();
            }

            return View(placesType);
        }

        // GET: PlacesTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlacesTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeId,TypeName")] PlacesType placesType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(placesType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(placesType);
        }

        // GET: PlacesTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placesType = await _context.PlacesTypes.FindAsync(id);
            if (placesType == null)
            {
                return NotFound();
            }
            return View(placesType);
        }

        // POST: PlacesTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeId,TypeName")] PlacesType placesType)
        {
            if (id != placesType.TypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(placesType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlacesTypeExists(placesType.TypeId))
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
            return View(placesType);
        }

        // GET: PlacesTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placesType = await _context.PlacesTypes
                .FirstOrDefaultAsync(m => m.TypeId == id);
            if (placesType == null)
            {
                return NotFound();
            }

            return View(placesType);
        }

        // POST: PlacesTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var placesType = await _context.PlacesTypes.FindAsync(id);
            if (placesType != null)
            {
                _context.PlacesTypes.Remove(placesType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlacesTypeExists(int id)
        {
            return _context.PlacesTypes.Any(e => e.TypeId == id);
        }
    }
}
