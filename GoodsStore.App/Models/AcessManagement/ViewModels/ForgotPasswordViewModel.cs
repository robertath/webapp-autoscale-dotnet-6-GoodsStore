using System.ComponentModel.DataAnnotations;

namespace GoodsStore.App.Models.AccessManagement.ViewModels
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
