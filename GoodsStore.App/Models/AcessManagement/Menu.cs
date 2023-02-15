using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace GoodsStore.App.Models.AccessManagement
{
    [DataContract]
    public class Menu : BaseModel
    {
        #region Properties
        [Required]
        [DataMember]
        public MainMenu MainMenu { get; set; }

        [Required]
        [DataMember]
        public DateTime DtRegistered { get; private set; }

        [Required]
        [DataMember]
        [Column(TypeName = "nvarchar(80)")]
        public string Title { get; private set; }

        [Required]
        [DataMember]
        [Column(TypeName = "nvarchar(80)")]
        public string Label { get; private set; }

        [AllowNull]
        [DataMember]
        public int? Sequence { get; private set; }

        [DataMember]
        [AllowNull]
        public string? Action { get; private set; }

        [DataMember]
        public List<Module> Modules { get; set; } = new List<Module>();
        
        [DataMember]
        public bool Enabled { get; private set; } = false;
        #endregion Properties


        #region Constructors
        public Menu()
        {
            MainMenu = new MainMenu();
        }

        public Menu(MainMenu mainMenu, DateTime dtRegistered, string title, string label, int? sequence, bool enabled)
        {
            MainMenu = mainMenu;
            DtRegistered = dtRegistered;
            Title = title;
            Label = label;
            Sequence = sequence;
            Enabled = enabled;
        }
        #endregion Constructors
    }
}
