using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

[Table("t_adopcion")]
public class Adopcion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AdopcionId { get; set; }

    [Required]
    public int MascotaId { get; set; }

    [Required]
    public int AdoptanteId { get; set; }

    public Mascota Mascota { get; set; }
    public Adoptante Adoptante { get; set; }
}
