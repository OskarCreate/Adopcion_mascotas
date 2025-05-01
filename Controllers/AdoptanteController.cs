using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Adopcion_mascotas.Data;

namespace Adopcion_mascotas.Controllers
{
    public class AdoptanteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdoptanteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Adoptante
        public async Task<IActionResult> Index()
        {
            return View(await _context.DbSetAdoptante.ToListAsync());
        }

        // GET: Adoptante/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptante = await _context.DbSetAdoptante
                .FirstOrDefaultAsync(m => m.AdoptanteId == id);
            if (adoptante == null)
            {
                return NotFound();
            }

            return View(adoptante);
        }

        // GET: Adoptante/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adoptante/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdoptanteId,NombreAdoptante,CorreoElectronico")] Adoptante adoptante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adoptante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adoptante);
        }

        // GET: Adoptante/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptante = await _context.DbSetAdoptante.FindAsync(id);
            if (adoptante == null)
            {
                return NotFound();
            }
            return View(adoptante);
        }

        // POST: Adoptante/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdoptanteId,NombreAdoptante,CorreoElectronico")] Adoptante adoptante)
        {
            if (id != adoptante.AdoptanteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adoptante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdoptanteExists(adoptante.AdoptanteId))
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
            return View(adoptante);
        }

        // GET: Adoptante/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptante = await _context.DbSetAdoptante
                .FirstOrDefaultAsync(m => m.AdoptanteId == id);
            if (adoptante == null)
            {
                return NotFound();
            }

            return View(adoptante);
        }

        // POST: Adoptante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adoptante = await _context.DbSetAdoptante.FindAsync(id);
            if (adoptante != null)
            {
                _context.DbSetAdoptante.Remove(adoptante);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptanteExists(int id)
        {
            return _context.DbSetAdoptante.Any(e => e.AdoptanteId == id);
        }
    }
}
