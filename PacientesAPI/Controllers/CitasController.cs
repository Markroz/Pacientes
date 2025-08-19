using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pacientes.DataBase.Managers;
using Pacientes.DataBase.Models.pacientes;
using Pacientes.DataBase.Utils;
using PacientesAPI.Models.Request;
using PacientesAPI.Utils;
using System.Globalization;

namespace PacientesAPI.Controllers
{
    [ApiController]
    public class CitasController : ControllerBase
    {

        private readonly DbContextAPI _context;
        private readonly IConfiguration _config;

        public CitasController(DbContextAPI context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [Authorize]
        [HttpGet("api/citas/")]
        public async Task<IActionResult> GetCitas([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var totalCount = await _context.Citas.CountAsync();
            var citas = await _context.Citas
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResult<Cita>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = citas
            };

            return Ok(result);
        }

        [Authorize]
        [HttpPost("api/citas/{cedula}")]
        public async Task<JsonResult> addCita(string cedula, RequestCita citaRequest)
        {

            DateTime fechaConvertida;

            bool esValida = CitaManager.ConvertirFecha(citaRequest.fecha, out fechaConvertida);

            if (!esValida)
            {
                return new JsonResult("Fecha en formato no valida") { StatusCode = StatusCodes.Status400BadRequest };
            }

            Cita newCita = new Cita
            {
                PacienteCedula = cedula,
                fecha = fechaConvertida,
                Tipo = citaRequest.Tipo,
            };
            
            var res = new CitaManager(_context).AddCita(cedula, newCita);

            if (!(bool)res[0])
            {
                return new JsonResult(res[1]) { StatusCode = StatusCodes.Status400BadRequest };
            }

            return new JsonResult(res[1]);
        }

        [Authorize]
        [HttpPut("api/citas/{id}")]
        public async Task<JsonResult> updateCita(int id, RequestCita citaRequest)
        {

            DateTime fechaConvertida;

            bool esValida = CitaManager.ConvertirFecha(citaRequest.fecha, out fechaConvertida);

            if (!esValida)
            {
                return new JsonResult("Fecha en formato no valida") { StatusCode = StatusCodes.Status400BadRequest };
            }

            Cita newCita = new Cita
            {
                Id = id,
                fecha = fechaConvertida,
                Tipo = citaRequest.Tipo,
            };

            var res = new CitaManager(_context).UpdateCitas(newCita);

            if (!(bool)res[0])
            {
                return new JsonResult(res[1]) { StatusCode = StatusCodes.Status400BadRequest };
            }

            return new JsonResult(res[1]);
        }

        [Authorize]
        [HttpDelete("api/citas/{id}")]
        public async Task<JsonResult> cancelarCita(int id)
        {
            var res = new CitaManager(_context).Cancelar(id);

            if (!(bool)res[0])
            {
                return new JsonResult(res[1]) { StatusCode = StatusCodes.Status400BadRequest };
            }

            return new JsonResult(res[1]);
        }
    }
}
