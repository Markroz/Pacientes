using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pacientes.DataBase.Models.pacientes
{
    public class Cita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(25)]
        public string Tipo { get; set; } = string.Empty;

        public DateTime fecha { get; set; }

        [MaxLength(25)]
        public string Estado { get; set; } = "Pendiente";

        [ForeignKey("Paciente")]
        [MaxLength(25)]
        public string PacienteCedula { get; set; } = string.Empty;
    }
}
