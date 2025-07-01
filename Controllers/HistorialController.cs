using Microsoft.AspNetCore.Mvc;
using System;

namespace Easycode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistorialController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HistorialController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getHistorial")]
        public IActionResult GetHistorial()
        {
            var historial = (from m in _context.Movimientos
                             join a in _context.Aprendices on m.IdAprendiz equals a.IdAprendiz into aprendizJoin
                             from a in aprendizJoin.DefaultIfEmpty()
                             join u in _context.Usuarios on m.IdUsuario equals u.IdUsuario into usuarioJoin
                             from u in usuarioJoin.DefaultIfEmpty()
                             join mm in _context.MovimientoMateriales on m.IdMovimiento equals mm.IdMovimiento into mmJoin
                             from mm in mmJoin.DefaultIfEmpty()
                             join mat in _context.Materiales on mm.IdMaterial equals mat.IdMaterial into matJoin
                             from mat in matJoin.DefaultIfEmpty()
                             join mv in _context.MovimientoVehiculos on m.IdMovimiento equals mv.IdMovimiento into mvJoin
                             from mv in mvJoin.DefaultIfEmpty()
                             join v in _context.Vehiculos on mv.IdVehiculo equals v.IdVehiculo into vJoin
                             from v in vJoin.DefaultIfEmpty()
                             join tv in _context.TipoVehiculos on v.IdTipoVehiculo equals tv.IdTipoVehiculo into tvJoin
                             from tv in tvJoin.DefaultIfEmpty()
                             select new
                             {
                                 Fecha = m.FechaHora.Date,
                                 Nombre = a != null ? a.Nombre : (u != null ? u.Nombre : "-"),
                                 TipoActor = a != null ? "Aprendiz" : (u != null ? "Usuario" : "-"),
                                 Documento = a != null ? a.Documento : (u != null ? u.Documento : "-"),
                                 FechaHora = m.FechaHora,
                                 Movimiento = m.MovimientoTipo,
                                 NombreMaterial = mat != null ? mat.Nombre : "-",
                                 Referencia = mat != null ? mat.Referencia : "-",
                                 Placa = v != null ? v.Placa : "-",
                                 TipoVehiculo = tv != null ? tv.Tipo : "-"
                             })
                             .OrderByDescending(h => h.Fecha)
                             .ThenByDescending(h => h.FechaHora)
                             .ToList();

            return Ok(historial);
        }

        [HttpGet("buscarSugerencias")]
        public IActionResult BuscarSugerencias([FromQuery] string term)
        {
            // Implementa aquí la lógica de sugerencias si la necesitas
            return Ok(new List<object>());
        }
    }
}