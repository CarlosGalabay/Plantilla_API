using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyecto.API.Entities.Productos;
using proyecto.API.Middleware.Exceptions;
using proyecto.API.Services;
using proyecto.API.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace proyecto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productos = await _productoService.GetProductosAsync();
            return Ok(productos);
        }

        [HttpGet("{id}"), Authorize(Roles = "empleado")]
        public async Task<IActionResult> Get(int id)
        {
            var producto = await _productoService.GetProductoByIdAsync(id);
            if (producto != null)
                return Ok(producto);
            throw new NotFoundException("Pilas no existe ese ID.\nPD: No soy de Computación xd");

        }

        [HttpGet("filtro_nombre"), Authorize(Roles = "administrador,empleado")]
        public async Task<IActionResult> FiltroNombre(string? nombre, bool? estado)
        {
            var productos = await _productoService.GetProductoByNombreAsync(nombre, estado);
            if (productos != null)
                return Ok(productos);
            return NotFound("No hay productos que coincidan con la busqueda.");
        }

        [HttpPost]
        public async Task<IActionResult> Post(Producto producto)
        {
            producto.Activo = true;
            _productoService.Add(producto);

            if (await _productoService.SaveAll())
                return Ok(producto);
            return BadRequest();
        }

        [HttpPut("{id}"), Authorize(Roles ="administrador")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody]ProductoUpdateDto productoDto)
        {
            if (id != productoDto.Id && productoDto.Id <= 0 && id <= 0)
                BadRequest("No existe el producto.");

            var productoToUpdate = await _productoService.GetProductoByIdAsync(productoDto.Id);

            if (productoToUpdate == null)
                return BadRequest();

            if (productoDto.Descripcion != null)
                productoToUpdate.Descripcion = productoDto.Descripcion;
            if (productoDto.Precio > 0)
                productoToUpdate.Precio = productoDto.Precio;

            if (!await _productoService.SaveAll())
                return NoContent();

            return Ok(productoToUpdate);

        }

    }
}
