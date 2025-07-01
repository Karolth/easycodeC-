using System;
using System.Collections.Generic;

namespace Easycode.Models
{
    public class TipoPrograma
    {
        public int IdTipoPrograma { get; set; }
        public string Tipo { get; set; }
        public ICollection<Programa> Programas { get; set; }
    }

    public class Programa
    {
        public int IdPrograma { get; set; }
        public string Nombre { get; set; }
        public string Version { get; set; }
        public DateTime Fecha { get; set; }
        public int IdTipoPrograma { get; set; }
        public TipoPrograma TipoPrograma { get; set; }
        public ICollection<Ficha> Fichas { get; set; }
    }

    public class Aprendiz
    {
        public int IdAprendiz { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string RH { get; set; }
        public ICollection<FichaAprendiz> FichaAprendiz { get; set; }
        public ICollection<Material> Materiales { get; set; }
        public ICollection<Vehiculo> Vehiculos { get; set; }
        public ICollection<Movimiento> Movimientos { get; set; }
        public ICollection<ImagenAprendiz> Imagenes { get; set; }
    }

    public class Ficha
    {
        public int IdFicha { get; set; }
        public int Numficha { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string Jornada { get; set; }
        public int IdPrograma { get; set; }
        public Programa Programa { get; set; }
        public ICollection<FichaAprendiz> FichaAprendiz { get; set; }
    }

    public class FichaAprendiz
    {
        public int IdFichaAprendiz { get; set; }
        public int IdFicha { get; set; }
        public int IdAprendiz { get; set; }
        public Ficha Ficha { get; set; }
        public Aprendiz Aprendiz { get; set; }
    }

    public class TipoVehiculo
    {
        public int IdTipoVehiculo { get; set; }
        public string Tipo { get; set; }
        public ICollection<Vehiculo> Vehiculos { get; set; }
    }

    public class Vehiculo
    {
        public int IdVehiculo { get; set; }
        public string Placa { get; set; }
        public int IdTipoVehiculo { get; set; }
        public int IdUsuario { get; set; }
        public int IdAprendiz { get; set; }
        public TipoVehiculo TipoVehiculo { get; set; }
        public Usuario Usuario { get; set; }
        public Aprendiz Aprendiz { get; set; }
        public ICollection<MovimientoVehiculo> MovimientoVehiculos { get; set; }
    }

    public class TipoMaterial
    {
        public int IdTipoMaterial { get; set; }
        public string Tipo { get; set; }
        public ICollection<Material> Materiales { get; set; }
    }

    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
        public ICollection<Material> Materiales { get; set; }
        public ICollection<Vehiculo> Vehiculos { get; set; }
        public ICollection<Movimiento> Movimientos { get; set; }
    }

    public class Rol
    {
        public int IdRol { get; set; }
        public string RolNombre { get; set; }
        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
    }

    public class UsuarioRol
    {
        public int IdUsuarioRol { get; set; }
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public Usuario Usuario { get; set; }
        public Rol Rol { get; set; }
    }

    public class Material
    {
        public int IdMaterial { get; set; }
        public string Nombre { get; set; }
        public string Referencia { get; set; }
        public string Marca { get; set; }
        public string Observaciones { get; set; }
        public int IdTipoMaterial { get; set; }
        public int IdUsuario { get; set; }
        public int IdAprendiz { get; set; }
        public TipoMaterial TipoMaterial { get; set; }
        public Usuario Usuario { get; set; }
        public Aprendiz Aprendiz { get; set; }
        public ICollection<MovimientoMaterial> MovimientoMateriales { get; set; }
    }

    public class Movimiento
    {
        public int IdMovimiento { get; set; }
        public DateTime FechaHora { get; set; }
        public string MovimientoTipo { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdAprendiz { get; set; }
        public Usuario Usuario { get; set; }
        public Aprendiz Aprendiz { get; set; }
        public ICollection<MovimientoMaterial> MovimientoMateriales { get; set; }
        public ICollection<MovimientoVehiculo> MovimientoVehiculos { get; set; }
    }

    public class MovimientoMaterial
    {
        public int IdMovimientoMaterial { get; set; }
        public string Estado { get; set; }
        public int IdMovimiento { get; set; }
        public int IdMaterial { get; set; }
        public Movimiento Movimiento { get; set; }
        public Material Material { get; set; }
    }

    public class MovimientoVehiculo
    {
        public int IdMovimientoVehiculo { get; set; }
        public string Estado { get; set; }
        public int IdMovimiento { get; set; }
        public int IdVehiculo { get; set; }
        public Movimiento Movimiento { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }

    public class ImagenAprendiz
    {
        public int IdImagen { get; set; }
        public string NombreArchivo { get; set; }
        public byte[] RutaArchivo { get; set; }
        public int IdAprendiz { get; set; }
        public Aprendiz Aprendiz { get; set;