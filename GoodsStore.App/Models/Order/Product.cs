using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodsStore.App.Models
{
    public class Product : BaseModel
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Product()
        {

        }

        public Product(string code, string name, decimal price)
        {
            Code = code;
            Name = name;
            Price = price;
        }

    }
}
