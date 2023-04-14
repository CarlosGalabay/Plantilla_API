using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyecto.API.Entities.Productos;

[Table("productos")]
public partial class Producto
{
    [Key]
    [Column("idproducto")]
    public int Idproducto { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    public string? Nombre { get; set; }

    [Column("descripcion")]
    [StringLength(255)]
    public string? Descripcion { get; set; }

    [Column("precio")]
    public decimal Precio { get; set; }

    [Column("activo")]
    public bool? Activo { get; set; }
}
