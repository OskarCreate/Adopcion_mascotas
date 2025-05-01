using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

[Table("t_mascota")]
public class Mascota
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MascotaId { get; set; }

    [Required]
    [MaxLength(100)]
    public string NombreMascota { get; set; }

    [Required]
    public string Tipo { get; set; } // Perro, gato, etc.

    [Required]
    [Range(0, 100)]
    public int Edad { get; set; }

    [Required]
    public string EstadoAdopcion { get; set; } = "disponible";

    public Adopcion? Adopcion { get; set; }
}

