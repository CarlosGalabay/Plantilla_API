using proyecto.API.Entities.Productos;

namespace proyecto.API.Services.Interfaces
{
    public interface IProductoService
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<Producto>> GetProductosAsync();
        Task<Producto> GetProductoByIdAsync(int id);
        Task<IEnumerable<Producto>> GetProductoByNombreAsync(string? nombre, bool? activo);
    }
}
