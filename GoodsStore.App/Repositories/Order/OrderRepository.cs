using GoodsStore.App.Infra;
using GoodsStore.App.Models;
using GoodsStore.App.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GoodsStore.App.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderRepository(DBContext context,
                                IHttpContextAccessor contextAccessor,
                                IOrderItemRepository orderItemRepository,
                                ICustomerRepository customerRepository) : base(context)
        {
            _contextAccessor = contextAccessor;
            _orderItemRepository = orderItemRepository;
            _customerRepository = customerRepository;
        }

        public async Task AddOrder(string code)
        {
            var product = await _context.Set<Product>()
                .Where(p => p.Code == code)
                .SingleOrDefaultAsync();

            if (product == null)
                throw new ArgumentException("Product not founded!");

            var order = await GetOrder();

            var orderItem = await _context.Set<OrderItem>()
                .Where(i => i.Product.Code == code && i.Order.Id == order.Id)
                .SingleOrDefaultAsync();

            if (orderItem == null)
            {
                orderItem = new OrderItem(order, product, 1, product.Price);
                await _context.Set<OrderItem>().AddAsync(orderItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Order> GetOrder()
        {
            var OrderId = GetOrderId();
            var order = await _dbSet
                        .Include(p => p.Items)
                            .ThenInclude(p => p.Product)
                        .Include(p => p.Customer)
                            .ThenInclude(p => p.User)
                        .Where(i => i.Id == OrderId)
                        .SingleOrDefaultAsync();

            if (order == null)
            {
                order = new Order();
                _dbSet.Add(order);
                await _context.SaveChangesAsync();
                SetOrderId(order.Id);
            }

            return order;
        }

        #region Private Methods
        private int? GetOrderId()
        {
            return _contextAccessor.HttpContext?.Session.GetInt32("OrderId");
        }

        private void SetOrderId(int OrderId)
        {
            _contextAccessor.HttpContext?.Session.SetInt32("OrderId", OrderId);
        }
        #endregion

        #region Public Methods
        public async Task<UpdateQuantityResponse> SetQuantity(OrderItem orderItem)
        {
            var orderItemDB = await _orderItemRepository.GetOrderItem(orderItem.Id);
            if (orderItemDB != null)
            {
                orderItemDB.UpdateQuantity(orderItem.Quantity);

                if (orderItem.Quantity == 0)
                    await _orderItemRepository.DeleteOrderItem(orderItem.Id);

                await _context.SaveChangesAsync();

                var order = await GetOrder();
                var cartViewModel = new CartViewModel(order.Items);

                return new UpdateQuantityResponse(cartViewModel, orderItemDB);
            }
            throw new ArgumentException("Order item not founded");
        }

        public async Task<Order> SetRegister(Customer customer)
        {
            var order = await GetOrder();
            await _customerRepository.Update(order.Customer.Id, customer);
            return order;
        }
        #endregion
    }
}
