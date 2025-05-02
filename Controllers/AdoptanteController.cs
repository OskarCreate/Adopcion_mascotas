using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; // Necesario para [Authorize]
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Adopcion_mascotas.Data;
using Adopcion_mascotas.Helpers;

namespace Adopcion_mascotas.Controllers
{
    public class AdoptanteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdoptanteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Solo Admin puede ver la lista
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.DbSetAdoptante.ToListAsync());
        }

        // Solo Admin puede ver detalles
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var adoptante = await _context.DbSetAdoptante
                .FirstOrDefaultAsync(m => m.AdoptanteId == id);

            if (adoptante == null)
                return NotFound();

            return View(adoptante);
        }

        // Cualquier usuario puede crear adoptante
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdoptanteId,NombreAdoptante,CorreoElectronico")] Adoptante adoptante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adoptante);
                await _context.SaveChangesAsync();

                // Guardar en la sesión
                HttpContext.Session.Set("AdoptanteRegistrado", adoptante);

                // O también puedes usar TempData (si solo necesitas mostrar un mensaje simple)
                TempData["AdoptanteNombre"] = adoptante.NombreAdoptante;

                return RedirectToAction("Privacy", "Home"); // Redirige al controlador Home
            }
            return View(adoptante);
        }

        // Solo Admin puede editar
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var adoptante = await _context.DbSetAdoptante.FindAsync(id);
            if (adoptante == null)
                return NotFound();

            return View(adoptante);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdoptanteId,NombreAdoptante,CorreoElectronico")] Adoptante adoptante)
        {
            if (id != adoptante.AdoptanteId)
                return NotFound();

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
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(adoptante);
        }

        // Solo Admin puede eliminar
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var adoptante = await _context.DbSetAdoptante
                .FirstOrDefaultAsync(m => m.AdoptanteId == id);
            if (adoptante == null)
                return NotFound();

            return View(adoptante);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adoptante = await _context.DbSetAdoptante.FindAsync(id);
            if (adoptante != null)
                _context.DbSetAdoptante.Remove(adoptante);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptanteExists(int id)
        {
            return _context.DbSetAdoptante.Any(e => e.AdoptanteId == id);
        }
    }
}

