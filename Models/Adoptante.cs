using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

[Table("t_adoptante")]
public class Adoptante
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AdoptanteId { get; set; }

    [Required]
    public string NombreAdoptante { get; set; }

    [Required]
    [EmailAddress]
    public string CorreoElectronico { get; set; }

    public ICollection<Adopcion> Adopciones { get; set; } = new List<Adopcion>();
}
