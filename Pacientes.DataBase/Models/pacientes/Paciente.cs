using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Pacientes.DataBase.Models.pacientes
{
    public class Paciente
    {
        [Key]
        [MaxLength(25), Required]
        public string Cedula { get; set; } = string.Empty;

        [MaxLength(25), Required]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required]
        public int Edad { get; set; }

        public List<Cita> Citas { get; set; } = new List<Cita>();

    }
}
