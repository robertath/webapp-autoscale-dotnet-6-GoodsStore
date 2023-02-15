using GoodsStore.App.Infra;
using GoodsStore.App.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodsStore.App.Repositories
{
    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(DBContext context) : base(context)
        {
        }

        public async Task<OrderItem> GetOrderItem(int orderItemId)
        {
            return await _dbSet
                    .Include(p=> p.Order)
                    .Where(i => i.Id == orderItemId)
                    .SingleOrDefaultAsync();
        }

        public async Task DeleteOrderItem(int id)
        {
            _dbSet.Remove(await GetOrderItem(id));
        }
    }
}
