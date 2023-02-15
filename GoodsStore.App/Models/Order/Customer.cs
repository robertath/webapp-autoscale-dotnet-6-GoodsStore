using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using GoodsStore.App.Models.AccessManagement;

namespace GoodsStore.App.Models
{
    public class Customer : BaseModel
    {
        [Required]
        [DataMember]
        public User User { get; private set; }
        
        [Required(ErrorMessage = "Address is mandatory.")]
        public string Address { get; set; } = "";
        [Required(ErrorMessage = "Number is mandatory.")]
        public string Number { get; set; } = "";
        [Required(ErrorMessage = "Neighborhod is mandatory.")]
        public string Neighborhood { get; set; } = "";
        [Required(ErrorMessage = "County is mandatory.")]
        public string County { get; set; } = "";
        [Required(ErrorMessage = "State is mandatory.")]
        public string State { get; set; } = "";
        [Required(ErrorMessage = "Postal Code is mandatory.")]
        public string PostalCode { get; set; } = "";

        public Customer()
        {
            User = new User();
        }

        public Customer(User user)
        {
            User = user;
        }

        internal void Update(Customer customer)
        {
            Neighborhood = customer.Neighborhood;
            PostalCode = customer.PostalCode;
            Number = customer.Number;
            Address = customer.Address;
            County = customer.County;
            State = customer.State;
        }
    }
}
