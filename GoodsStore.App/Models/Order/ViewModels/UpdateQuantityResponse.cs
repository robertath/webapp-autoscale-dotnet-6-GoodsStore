namespace GoodsStore.App.Models.ViewModels
{
    public class UpdateQuantityResponse
    {
        public OrderItem OrderItem { get; set; }
        public CartViewModel CartViewModel { get; set; }

        public UpdateQuantityResponse(CartViewModel cartViewModel, OrderItem orderItem)
        {
            CartViewModel = cartViewModel;
            OrderItem = orderItem;
        }
    }
}
