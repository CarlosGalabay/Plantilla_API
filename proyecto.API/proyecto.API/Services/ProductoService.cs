using Microsoft.EntityFrameworkCore;
using proyecto.API.Entities;
using proyecto.API.Entities.Productos;
using proyecto.API.Entities.Usuarios;
using proyecto.API.Services.Interfaces;

namespace proyecto.API.Services
{
    public class ProductoService: IProductoService
    {
        private readonly NombreDbContext _dbContext;

        public ProductoService(NombreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add<T>(T entity) where T : class
        {
            _dbContext.Add(entity);

        }
        public void Delete<T>(T entity) where T : class
        {
            _dbContext.Remove(entity);

        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            return await _dbContext.Productos.FirstOrDefaultAsync(x => x.Idproducto == id);
        }

        public async Task<IEnumerable<Producto>> GetProductoByNombreAsync(string? nombre, bool? activo)
        {
            activo ??= true;
            var productos = await _dbContext.Productos.ToListAsync();
            if (nombre != null)
            {
                productos = productos.Where(x => x.Nombre.ToLower().Contains(nombre) && x.Activo == activo).ToList();
            }
            return productos;
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            var productos = await _dbContext.Productos.ToListAsync(); 
            return productos;
        }

        public async Task<bool> SaveAll()
        {
            return await _dbContext.SaveChangesAsync() > 0;

        }

    }
}
