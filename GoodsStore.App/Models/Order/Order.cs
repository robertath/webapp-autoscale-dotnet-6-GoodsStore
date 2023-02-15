using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace GoodsStore.App.Models
{
    [DataContract]
    public class Order : BaseModel
    {
        #region Properties
        [DataMember]
        public List<OrderItem> Items { get; private set; } = new List<OrderItem>();

        [DataMember]
        [Required]
        public virtual Customer Customer { get; private set; }

        [DataMember]
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }

        #endregion Properties

        #region Constructors
        public Order()
        {
            Customer = new Customer();
        }

        public Order(Customer register)
        {
            Customer = register;
        }
        #endregion Constructors
    }
}