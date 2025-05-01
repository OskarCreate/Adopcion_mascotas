using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Adopcion_mascotas.Models;

namespace Adopcion_mascotas.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Adopcion> DbSetAdopcion { get; set; }
    public DbSet<Adoptante> DbSetAdoptante { get; set; }
    public DbSet<Mascota> DbSetMascota { get; set; }
}
