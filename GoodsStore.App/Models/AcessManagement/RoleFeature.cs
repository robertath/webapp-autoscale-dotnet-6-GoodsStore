using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace GoodsStore.App.Models.AccessManagement
{
    [DataContract]
    public class RoleFeature : BaseModel
    {
        #region Properties
        [DataMember]
        [Required]
        public IdentityRole Role { get; private set; }

        [DataMember]
        [Required]
        public Feature Feature { get; private set; }

        [DataMember]
        [Required]
        public bool CanRead { get; private set; }

        [DataMember]
        [Required]
        public bool CanAdd { get; private set; }

        [DataMember]
        [Required]
        public bool CanEdit { get; private set; }

        [DataMember]
        [Required]
        public bool CanDelete { get; private set; }
        #endregion Properties

        #region Constructors
        public RoleFeature()
        {

        }

        public RoleFeature(IdentityRole role, Feature feature, bool canRead, bool canAdd, bool canEdit, bool canDelete)
        {
            Role = role;
            Feature = feature;
            CanRead = canRead;
            CanAdd = canAdd;
            CanEdit = canEdit;
            CanDelete = canDelete;
        }

        #endregion Constructors
    }
}
