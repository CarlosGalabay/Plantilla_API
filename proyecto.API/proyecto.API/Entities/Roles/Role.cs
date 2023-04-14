using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using proyecto.API.Entities.Usuarios;

namespace proyecto.API.Entities.Roles;

[Table("roles")]
public partial class Role
{
    [Key]
    [Column("idrol")]
    public int Idrol { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    public string? Descripcion { get; set; }

    [InverseProperty("IdrolNavigation")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
