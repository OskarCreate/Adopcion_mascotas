using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("t_mascota")]
public class Mascota
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MascotaId { get; set; }

    [Required(ErrorMessage = "Debe registrar un nombre")]
    [MaxLength(100)]
    public string NombreMascota { get; set; }

    [Required(ErrorMessage = "Debe registrar un tipo de mascota (gato, perro, etc.)")]
    public string Tipo { get; set; }

    [Required(ErrorMessage = "Debe registrar la edad de la mascota")]
    [Range(0, 100)]
    public int Edad { get; set; }

    [Required(ErrorMessage = "Debe registrar su estado de adopción (disponible/adoptado)")]
    public string EstadoAdopcion { get; set; } = "disponible";

    [Url(ErrorMessage = "Debe proporcionar una URL válida de imagen")]
    public string? ImageURL { get; set; }  // Es opcional ahora

    public Adopcion? Adopcion { get; set; }
}