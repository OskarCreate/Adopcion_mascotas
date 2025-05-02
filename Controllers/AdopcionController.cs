using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Adopcion_mascotas.Data;
using Microsoft.AspNetCore.Authorization;

namespace Adopcion_mascotas.Controllers
{
    public class AdopcionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdopcionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Adopcion
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DbSetAdopcion.Include(a => a.Adoptante).Include(a => a.Mascota);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Adopcion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adopcion = await _context.DbSetAdopcion
                .Include(a => a.Adoptante)
                .Include(a => a.Mascota)
                .FirstOrDefaultAsync(m => m.AdopcionId == id);
            if (adopcion == null)
            {
                return NotFound();
            }

            return View(adopcion);
        }

        // GET: Adopcion/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["AdoptanteId"] = new SelectList(_context.DbSetAdoptante, "AdoptanteId", "NombreAdoptante");
            ViewData["MascotaId"] = new SelectList(_context.DbSetMascota, "MascotaId", "NombreMascota");

            return View();
        }

        // POST: Adopcion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Adopcion adopcion)
        {
            var adopciones = _context.DbSetAdopcion.ToList();
            Console.WriteLine($"Total adopciones: {adopciones.Count}");

            Console.WriteLine($"MascotaId: {adopcion.MascotaId}, AdoptanteId: {adopcion.AdoptanteId}");

            if (ModelState.IsValid)
            {
                Console.WriteLine($"¿ModelState válido?: {ModelState.IsValid}");
                Console.WriteLine($"Estado de la entidad antes de agregar: {_context.Entry(adopcion).State}");
                _context.DbSetAdopcion.Add(adopcion);
                Console.WriteLine($"Estado después de agregar: {_context.Entry(adopcion).State}");

                try
                {
                var mascota = await _context.DbSetMascota.FindAsync(adopcion.MascotaId);
                mascota.EstadoAdopcion = "Adoptado";
                var result = await _context.SaveChangesAsync();
                Console.WriteLine($"Filas afectadas: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar: {ex}");
                }
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Error de modelo: {error.ErrorMessage}");
            }

            ViewData["AdoptanteId"] = new SelectList(_context.DbSetAdoptante, "AdoptanteId", "NombreAdoptante", adopcion.AdoptanteId);
            ViewData["MascotaId"] = new SelectList(_context.DbSetMascota, "MascotaId", "NombreMascota", adopcion.MascotaId);
            return View(adopcion);
        }



        // GET: Adopcion/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adopcion = await _context.DbSetAdopcion.FindAsync(id);
            if (adopcion == null)
            {
                return NotFound();
            }
            ViewData["AdoptanteId"] = new SelectList(_context.DbSetAdoptante, "AdoptanteId", "NombreAdoptante", adopcion.AdoptanteId);
            ViewData["MascotaId"] = new SelectList(_context.DbSetMascota, "MascotaId", "NombreMascota", adopcion.MascotaId);

            return View(adopcion);
        }

        // POST: Adopcion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("AdopcionId,MascotaId,AdoptanteId")] Adopcion adopcion)
        {
            if (id != adopcion.AdopcionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adopcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdopcionExists(adopcion.AdopcionId))
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
            ViewData["AdoptanteId"] = new SelectList(_context.DbSetAdoptante, "AdoptanteId", "NombreAdoptante", adopcion.AdoptanteId);
            ViewData["MascotaId"] = new SelectList(_context.DbSetMascota, "MascotaId", "NombreMascota", adopcion.MascotaId);

            return View(adopcion);
        }

        // GET: Adopcion/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adopcion = await _context.DbSetAdopcion
                .Include(a => a.Adoptante)
                .Include(a => a.Mascota)
                .FirstOrDefaultAsync(m => m.AdopcionId == id);
            if (adopcion == null)
            {
                return NotFound();
            }

            return View(adopcion);
        }

        // POST: Adopcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adopcion = await _context.DbSetAdopcion.FindAsync(id);
            if (adopcion != null)
            {
                _context.DbSetAdopcion.Remove(adopcion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdopcionExists(int id)
        {
            return _context.DbSetAdopcion.Any(e => e.AdopcionId == id);
        }
    }
}
