namespace proyecto.API.Entities.Productos
{
    public class ProductoUpdateDto
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
    }
}
