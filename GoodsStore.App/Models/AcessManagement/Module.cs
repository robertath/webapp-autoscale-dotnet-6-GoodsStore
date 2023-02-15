using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace GoodsStore.App.Models.AccessManagement
{
    [DataContract]
    public class Module : BaseModel
    {
        #region Properties
        [DataMember]
        [Required]
        public Menu Menu { get; private set; }
                
        [DataMember]
        [Required]
        public DateTime DtRegistered { get; private set; }

        [DataMember]
        [Required]
        [Column(TypeName = "nvarchar(80)")]
        public string Title { get; private set; }

        [DataMember]
        [Required]
        [Column(TypeName = "nvarchar(80)")]
        public string Label { get; private set; }

        [DataMember]
        [AllowNull]
        public string? Action { get; private set; }

        [DataMember]
        [AllowNull]
        public int? Sequence { get; private set; }

        [DataMember]
        public bool Enabled { get; private set; } = false;

        [DataMember]
        public List<Feature> Features { get; private set; } = new List<Feature>();

        #endregion Properties

        #region Constructors
        public Module()
        {

        }

        public Module(Menu menu, DateTime dtRegistered, string title, string label, string? action, int? sequence, bool enabled)
        {
            Menu = menu;
            DtRegistered = dtRegistered;
            Title = title;
            Label = label;
            Action = action;
            Sequence = sequence;
            Enabled = enabled;
        }
        #endregion Constructors
    }
}
