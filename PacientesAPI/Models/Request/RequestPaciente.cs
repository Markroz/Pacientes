using System.ComponentModel.DataAnnotations;

namespace PacientesAPI.Models.Request
{
    public class RequestPaciente
    {
        [Required]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required]
        public int Edad { get; set; }
    }
}
