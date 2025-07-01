using Microsoft.AspNetCore.Mvc;
using System;

namespace Easycode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CargarProgramasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CargarProgramasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var programas = _context.Programas
                .Select(p => new { p.IdPrograma, p.Nombre })
                .ToList();

            if (programas.Any())
                return Ok(new { success = true, data = programas });
            else
                return Ok(new { success = false, message = "No se encontraron programas de formación." });
        }
    }
}