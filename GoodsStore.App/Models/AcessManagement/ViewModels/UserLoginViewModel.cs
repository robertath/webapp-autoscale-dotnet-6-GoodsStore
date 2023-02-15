using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GoodsStore.App.Models.AccessManagement.ViewModels
{
    public class UserLoginViewModel
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }        
    }
}
