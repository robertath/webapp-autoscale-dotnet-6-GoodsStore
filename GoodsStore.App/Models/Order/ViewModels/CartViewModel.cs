namespace GoodsStore.App.Models.ViewModels
{
    public class CartViewModel
    {
        public IList<OrderItem> OrderItems { get; private set; }


        public CartViewModel(IList<OrderItem> items)
        {
            OrderItems = items;
        }

        public decimal Total => OrderItems.Sum(i => i.Quantity * i.UnitPrice);
    }
}
