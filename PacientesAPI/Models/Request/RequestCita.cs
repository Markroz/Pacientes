using System.ComponentModel.DataAnnotations;

namespace PacientesAPI.Models.Request
{
    public class RequestCita
    {
        [MaxLength(25), Required]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        public string fecha { get; set; }

        [MaxLength(25), Required]
        public string Estado { get; set; } = "Pendiente";
    }
}
