using System.ComponentModel.DataAnnotations;

namespace GoodsStore.App.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        [Required(ErrorMessage = "Address is mandatory.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Number is mandatory.")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Neighborhod is mandatory.")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "County is mandatory.")]
        public string County { get; set; }

        [Required(ErrorMessage = "State is mandatory.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal Code is mandatory.")]
        public string PostalCode { get; set; }


        public RegisterViewModel SetToView(Customer customer)
        {
            return new RegisterViewModel()
            {
                Name = $"{customer.User.FirstName} {customer.User.LastName}",
                Telephone = customer.User.PhoneNumber,
                Address = customer.Address,
                Number = customer.Number,
                Neighborhood = customer.Neighborhood,
                County = customer.County,
                State = customer.State,
                PostalCode = customer.PostalCode
            };
        }

        public Customer SetToObject(RegisterViewModel fromView, Customer customer)
        {
            customer.Address = fromView.Address;
            customer.Number = fromView.Number;
            customer.Neighborhood = fromView.Neighborhood;
            customer.County = fromView.County;
            customer.State = fromView.State;
            customer.PostalCode = fromView.PostalCode;
            return customer;
        }
    }
}
