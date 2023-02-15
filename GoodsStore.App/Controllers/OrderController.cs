using GoodsStore.App.Models;
using GoodsStore.App.Models.ViewModels;
using GoodsStore.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GoodsStore.App.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        public OrderController(IOrderRepository orderRepository, IProductRepository productRepository, IOrderItemRepository orderItemRepository, ICustomerRepository customerRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderItemRepository = orderItemRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Carousel()
        {
            return View(await _productRepository.GetProducts());
        }
                
        public async Task<IActionResult> Cart(string code)
        {            
            if (!string.IsNullOrEmpty(code))
                await _orderRepository.AddOrder(code);

            Order order = await _orderRepository.GetOrder();
            List<OrderItem> items = order.Items;
            CartViewModel cartViewModel = new CartViewModel(items);
            return View(cartViewModel);
        }
                
        public async Task<IActionResult> Register()
        {
            var order = await _orderRepository.GetOrder();

            if (order == null)
                return RedirectToAction("Carousel");

            var viewModel = new RegisterViewModel().SetToView(order.Customer);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Summary(RegisterViewModel register)
        {                        
            if (ModelState.IsValid)
            {
                Order order = await _orderRepository.GetOrder();
                var view = new RegisterViewModel();
                var customer = new RegisterViewModel().SetToObject(register, order.Customer);

                return View(await _orderRepository.SetRegister(customer));
            }

            return RedirectToAction("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<UpdateQuantityResponse> SetQuantity([FromBody]OrderItem data)
        {
            return await _orderRepository.SetQuantity(data);
        }


    }
}
