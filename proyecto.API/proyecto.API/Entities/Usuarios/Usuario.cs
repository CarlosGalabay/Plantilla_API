using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using proyecto.API.Entities.Roles;

namespace proyecto.API.Entities.Usuarios;

[Table("usuarios")]
public partial class Usuario
{
    [Key]
    [Column("idusuario")]
    public int Idusuario { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string? Nombre { get; set; }

    [Column("username")]
    [StringLength(200)]
    public string Username { get; set; } = null!;

    [Column("clave")]
    [StringLength(100)]
    public string Clave { get; set; } = null!;

    [Column("estado")]
    [StringLength(1)]
    public string? Estado { get; set; }

    [Column("idrol")]
    public int Idrol { get; set; }

    [ForeignKey("Idrol")]
    [InverseProperty("Usuarios")]
    public virtual Role IdrolNavigation { get; set; } = null!;
}
