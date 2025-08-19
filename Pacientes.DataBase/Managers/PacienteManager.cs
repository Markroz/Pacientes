using Newtonsoft.Json;
using Pacientes.DataBase.Models.pacientes;
using Pacientes.DataBase.Utils;

namespace Pacientes.DataBase.Managers
{
    public class PacienteManager
    {
        private readonly DbContextAPI _context;

        public PacienteManager(DbContextAPI context)
        {
            _context = context;
        }

        public Object[] AddPaciente(Paciente paciente)
        {
            try
            {
                var pacienteExiste = _context.Pacientes.FirstOrDefault(p => p.Cedula.Equals(paciente.Cedula));

                if (pacienteExiste == null)
                {

                    if(paciente.Edad < 0 || paciente.Edad > 100) return [false, "La edad del paciente es invalida, debe estar en el rango de 0 a 100"];

                    _context.Pacientes.Add(paciente);
                    _context.SaveChanges();
                    return [true, "Paciente agregado con exito"];
                }
                else
                {
                    return [false, string.Format("El paciente ya existe, no fue agregado {0}", JsonConvert.SerializeObject(pacienteExiste))];
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);

                return [false, string.Format("Error al agregar paciente: ", ex.InnerException)]
            ; ;
            }
        }

        public Object[] UpdatePaciente(Paciente paciente)
        {
            try
            {
                var pacienteExiste = _context.Pacientes.FirstOrDefault(p => p.Cedula.Equals(paciente.Cedula));

                if (pacienteExiste != null)
                {

                    if (paciente.Edad < 0 || paciente.Edad > 100) return [false, "La edad del paciente es invalida, debe estar en el rango de 0 a 100"];

                    pacienteExiste.NombreCompleto = paciente.NombreCompleto;
                    pacienteExiste.Edad = paciente.Edad;
                   
                    _context.Pacientes.Update(pacienteExiste);

                    _context.SaveChanges();
                    return [true, "Paciente Actualizado con exito"];
                }
                else
                {
                    return [false, string.Format("El paciente no existe")];
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return [false, string.Format("Error al actualizar el paciente: ", ex.InnerException)]
            ; ;
            }
        }

        public Object[] DeletePaciente(string cedula)
        {
            try
            {
                var pacienteExiste = _context.Pacientes.FirstOrDefault(p => p.Cedula.Equals(cedula));

                if (pacienteExiste != null)
                {
                    _context.Citas.RemoveRange(_context.Citas.Where(c => c.PacienteCedula == cedula));
                    _context.SaveChanges();

                    pacienteExiste.Citas = null;
                    _context.Pacientes.Remove(pacienteExiste);

                    _context.SaveChanges();
                    return [true, "Paciente Eliminado con exito"];
                }
                else
                {
                    return [false, string.Format("El paciente no existe")];
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return [false, string.Format("Error al eliminar el paciente: ", ex.InnerException)]
            ; ;
            }
        }
    }
}
