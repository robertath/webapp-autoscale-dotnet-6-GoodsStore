using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GoodsStore.App.Models
{
    [DataContract]
    public class OrderItem : BaseModel
    {
        #region Properties
        [Required]
        [DataMember]
        public Order Order { get; private set; }

        [Required]
        [DataMember]
        public Product Product { get; private set; }

        [Required]
        [DataMember]
        public int Quantity { get; private set; }

        [Required]
        [DataMember]
        public decimal UnitPrice { get; private set; }

        [DataMember]
        public decimal SubTotal => Quantity * UnitPrice;

        #endregion Properties

        #region Constructors
        public OrderItem()
        {

        }

        public OrderItem(Order order, Product product, int quantity, decimal unitPrice)
        {
            Order = order;
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
        #endregion Constructors

        #region Internal Methods
        internal void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
        }

        #endregion Internal Methods
    }
}