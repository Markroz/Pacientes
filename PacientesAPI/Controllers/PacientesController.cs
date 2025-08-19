using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pacientes.DataBase.Managers;
using Pacientes.DataBase.Models.pacientes;
using Pacientes.DataBase.Utils;
using PacientesAPI.Models.Request;
using PacientesAPI.Utils;
using System.Linq;

namespace PacientesAPI.Controllers
{
    [ApiController]
    public class PacientesController : ControllerBase
    {

        private readonly DbContextAPI _context;
        private readonly IConfiguration _config;

        public PacientesController(DbContextAPI context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [Authorize]
        [HttpGet("api/pacientes/")]
        public async Task<IActionResult> getPacientes([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var totalCount = await _context.Pacientes.CountAsync();
            var pacientes = await _context.Pacientes
                .Include(p => p.Citas)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResult<Paciente>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = pacientes
            };

            return Ok(result);
        }

        [Authorize]
        [HttpPost("api/pacientes/")]
        public async Task<JsonResult> createdPacientes(Paciente paciente)
        {
            
            PacienteManager pacienteManager = new PacienteManager(_context);
            object[] res = pacienteManager.AddPaciente(paciente);

            if (!(bool)res[0])
            {
                return new JsonResult(res[1]) { StatusCode = StatusCodes.Status400BadRequest };
            }

            return new JsonResult(res[1]);
        }

        [Authorize]
        [HttpGet("api/pacientes/{cedula}")]
        public async Task<JsonResult> getPaciente(string cedula)
        {

            var pacienteExiste = _context.Pacientes.Include(p => p.Citas).FirstOrDefault(p => p.Cedula.Equals(cedula));

            if (pacienteExiste == null)
            {
                return new JsonResult("Paciente no existe") { StatusCode = StatusCodes.Status400BadRequest };
            }

            return new JsonResult(pacienteExiste);
        }

        [Authorize]
        [HttpPut("api/pacientes/{cedula}")]
        public async Task<JsonResult> UpdatePacientes(string cedula, RequestPaciente requesPaciente)
        {
            Paciente paciente = new Paciente
            {
                Cedula = cedula,
                NombreCompleto = requesPaciente.NombreCompleto,
                Edad = requesPaciente.Edad
            };

            PacienteManager pacienteManager = new PacienteManager(_context);
            object[] res = pacienteManager.UpdatePaciente(paciente);

            if (!(bool)res[0])
            {
                return new JsonResult(res[1]) { StatusCode = StatusCodes.Status400BadRequest };
            }

            return new JsonResult(res[1]);
        }

        [Authorize]
        [HttpDelete("api/pacientes/{cedula}")]
        public async Task<JsonResult> DeletePacientes(string cedula)
        {

            PacienteManager pacienteManager = new PacienteManager(_context);
            object[] res = pacienteManager.DeletePaciente(cedula);

            if (!(bool)res[0])
            {
                return new JsonResult(res[1]) { StatusCode = StatusCodes.Status400BadRequest };
            }

            return new JsonResult(res[1]);
        }
    }
}
