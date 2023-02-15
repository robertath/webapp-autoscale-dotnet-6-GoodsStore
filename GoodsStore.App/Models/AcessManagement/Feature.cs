using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace GoodsStore.App.Models.AccessManagement
{
    [DataContract]
    public class Feature : BaseModel
    {
        #region Properties
        [DataMember]
        [Required]
        public Module Module { get; private set; }

        [DataMember]
        [Required]
        public DateTime DtRegistered { get; private set; }
                
        [DataMember]
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Title { get; private set; }

        [DataMember]
        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Label { get; private set; }

        [DataMember]
        [AllowNull]
        public string? Action { get; private set; }
                
        [DataMember]
        [Required]
        public bool Enabled { get; private set; } = false;
        #endregion Properties

        #region Constructors
        public Feature()
        {

        }

        public Feature(Module module, DateTime dtRegistered, string title, string label, string? action, bool enabled)
        {
            Module = module;
            DtRegistered = dtRegistered;
            Title = title;
            Label = label;
            Action = action;
            Enabled = enabled;
        }
        #endregion Constructors
    }
}
