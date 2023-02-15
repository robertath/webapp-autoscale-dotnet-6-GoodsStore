using GoodsStore.App.Models;
using GoodsStore.App.Models.ViewModels;

namespace GoodsStore.App.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrder();
        Task AddOrder(string code);
        Task<UpdateQuantityResponse> SetQuantity(OrderItem orderItem);
        Task<Order> SetRegister(Customer customer);
    }

}