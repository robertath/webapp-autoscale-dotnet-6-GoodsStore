using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace GoodsStore.App.Models.AccessManagement
{
    [DataContract]
    public class MainMenu : BaseModel
    {
        #region Properties
        [Required]
        [DataMember]
        public DateTime DtRegistered { get; set; }

        [Required]
        [DataMember]
        [Column(TypeName = "nvarchar(80)")]
        public string Title { get; set; }

        [DataMember]
        [Required]
        [Column(TypeName = "nvarchar(80)")]
        public string Label { get; set; }

        [DataMember]
        [AllowNull]
        public int? Sequence { get; set; }

        [DataMember]
        public bool Enabled { get; set; } = false;

        [DataMember]
        [AllowNull]
        public string? Action { get; set; }

        [DataMember]
        public List<Menu> Menus { get; private set; } = new List<Menu>();
        #endregion Properties

        #region Constructors
        public MainMenu()
        {

        }

        public MainMenu(DateTime dtRegistered, string title, string label, int? sequence, string? action, bool enabled)
        {
            DtRegistered = dtRegistered;
            Title = title;
            Label = label;
            Sequence = sequence;
            Action = action;
            Enabled = enabled;
        }
        #endregion Constructors
    }
}
