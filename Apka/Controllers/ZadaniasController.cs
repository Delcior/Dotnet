using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apka.Models;

namespace Apka.Controllers
{
    public class ZadaniasController : Controller
    {
        private readonly apka_dbContext _context;

        public ZadaniasController(apka_dbContext context)
        {
            _context = context;
        }

        // GET: Zadanias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Zadania.ToListAsync());
        }

        // GET: Zadanias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zadania = await _context.Zadania
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zadania == null)
            {
                return NotFound();
            }

            return View(zadania);
        }

        // GET: Zadanias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zadanias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Data")] Zadania zadania)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zadania);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zadania);
        }

        // GET: Zadanias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zadania = await _context.Zadania.FindAsync(id);
            if (zadania == null)
            {
                return NotFound();
            }
            return View(zadania);
        }

        // POST: Zadanias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Data")] Zadania zadania)
        {
            if (id != zadania.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zadania);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZadaniaExists(zadania.Id))
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
            return View(zadania);
        }

        // GET: Zadanias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zadania = await _context.Zadania
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zadania == null)
            {
                return NotFound();
            }

            return View(zadania);
        }

        // POST: Zadanias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zadania = await _context.Zadania.FindAsync(id);
            _context.Zadania.Remove(zadania);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZadaniaExists(int id)
        {
            return _context.Zadania.Any(e => e.Id == id);
        }
    }
}
