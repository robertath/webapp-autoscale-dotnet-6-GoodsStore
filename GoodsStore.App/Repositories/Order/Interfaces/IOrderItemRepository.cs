using GoodsStore.App.Models;

namespace GoodsStore.App.Repositories
{
    public interface IOrderItemRepository
    {        
        Task<OrderItem> GetOrderItem(int orderItemId);
        Task DeleteOrderItem(int id);
    }
}