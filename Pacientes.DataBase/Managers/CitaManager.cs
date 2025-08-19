using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pacientes.DataBase.Models.pacientes;
using Pacientes.DataBase.Utils;
using System.Globalization;

namespace Pacientes.DataBase.Managers
{
    public class CitaManager
    {
        private readonly DbContextAPI _context;

        public CitaManager(DbContextAPI context)
        {
            _context = context;
        }

        public static bool ConvertirFecha(string fecha, out DateTime fechaConvertida)
        {
            string formato = "dd/MM/yyyy hh:mm tt";
            return DateTime.TryParseExact(fecha,formato,CultureInfo.InvariantCulture,DateTimeStyles.None,out fechaConvertida);
        }

        public Object[] AddCita(string cedula, Cita cita)
        {
            try
            {
                var pacienteExiste = _context.Pacientes.FirstOrDefault(p => p.Cedula.Equals(cedula));

                if (pacienteExiste != null)
                {
                    cita.PacienteCedula = cedula;

                    _context.Citas.Add(cita);
                   
                    
                    pacienteExiste.Citas.Add(cita);

                    _context.SaveChanges();

                    return [true, "Cita agregada con exito"];
                }
                else
                {
                    return [false, "El paciente no existe"];
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);

                return [false, string.Format("Error al agregar cita: ", ex.InnerException)]
            ; ;
            }
        }

        public Object[] UpdateCitas(Cita cita)
        {
            try
            {
                var citaExistente = _context.Citas.FirstOrDefault(p => p.Id == cita.Id);

                if (citaExistente != null)
                {
                    citaExistente.Tipo = cita.Tipo;
                    citaExistente.fecha = cita.fecha;
                    citaExistente.Estado = cita.Estado;

                    _context.Citas.Update(citaExistente);

                    _context.SaveChanges();
                    return [true, "Cita Actualizada con exito"];
                }
                else
                {
                    return [false, string.Format("La Cita no existe")];
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return [false, string.Format("Error al actualizar la cita: ", ex.InnerException)]
            ; ;
            }
        }

        public Object[] Cancelar(int id)
        {
            try
            {
                var citaExistente = _context.Citas.FirstOrDefault(p => p.Id == id);

                if (citaExistente != null)
                {
                    citaExistente.Estado = "Cancelada";

                    _context.Citas.Update(citaExistente);

                    _context.SaveChanges();
                    return [true, "Cita Cancelada con exito"];
                }
                else
                {
                    return [false, string.Format("La Cita no existe")];
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return [false, string.Format("Error al cancelar la cita: ", ex.InnerException)]
            ; ;
            }
        }
    }
}
