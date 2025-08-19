using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pacientes.DataBase.Models.Usuarios
{
    public class Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(150)]
        public string Usuario { get; set; } = string.Empty;
        [MaxLength(150)]
        public string Contrasenia { get; set; } = string.Empty;
        [MaxLength(25)]
        public string Rol { get; set; } = "Usuario";
    }
}
