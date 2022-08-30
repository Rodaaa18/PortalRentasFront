using System.ComponentModel.DataAnnotations;

namespace AppWebRentas.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        [EmailAddress]
        public string Usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "¿Recordar cuenta?")]
    }
}
