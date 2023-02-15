using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace GoodsStore.App.Models.AccessManagement
{
    public class User : IdentityUser
    {
        [AllowNull]
        [Column(TypeName = "nvarchar(200)")]
        public string? FirstName { get; set; }
        [AllowNull]
        [Column(TypeName = "nvarchar(200)")]
        public string? LastName { get; set; }
    }
}
