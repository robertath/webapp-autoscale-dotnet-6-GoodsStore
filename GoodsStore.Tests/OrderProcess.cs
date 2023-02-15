namespace GoodsStore.Tests
{
    public class OrderProcess
    {
        ////private readonly IOrderRepository _orderRepository;
        ////private readonly DataService _dataService;

        //#region Fake objects
        //private List<Product> _products;
        //private List<Customer> _customers;
        //private List<Order> _orders;
        //private List<OrderItem> _orderItems;
        //#endregion

        //public OrderProcess()
        //{
        //    _products = new List<Product>();
        //    _customers = new List<Customer>();
        //    //    var service = new ServiceCollection();
        //    //    service.AddTransient<IDataService, DataService>();
        //    //    service.AddTransient<IOrderRepository, OrderRepository>();
        //}

        //[Fact]
        //public void AddProductsAutoImport()
        //{
        //    //Arrange
        //    var json = File.ReadAllText(Path.Combine(
        //        "D:\\Projects\\WebApps\\NetCore\\GoodsStore\\GoodsStore.App\\wwwroot\\data",                 
        //        "books.json"));
        //    List<ProductsByImport>? books = JsonConvert.DeserializeObject<List<ProductsByImport>>(json);
                        
        //    //Act
        //    foreach (var item in books)
        //    {
        //        if (!_products.Where(i => i.Code == item.Code).Any())
        //            _products.Add(new Product(item.Code, item.Name, item.Price));
        //    }
        //    var count = _products.Count();

        //    //Assert
        //    Assert.Equal(65, count);
        //}

        //[Fact]
        //public void RegisterCustomer()
        //{
        //    //arrange
        //    Customer customer1 = new Customer { Email = "customer1@teste.com", Name = "Customer 1", Address = "Av. Paulista", Telephone = "111111111", Number = "100", Neighborhood = "Bela Vista", PostalCode = "10000-000", State = "SP" };
        //    Customer customer2 = new Customer { Email = "customer2@teste.com", Name = "Customer 2", Address = "Av. Paulista", Telephone = "222222222", Number = "200", Neighborhood = "Bela Vista", PostalCode = "10000-001", State = "SP" };
        //    _customers.Add(customer1);
        //    _customers.Add(customer2);

        //    //act
        //    var countIncluded = _customers.Count();

        //    //assert
        //    Assert.Equal(2, countIncluded);
        //}

        //[Theory]
        //[InlineData(null, "001", 1)]
        //[InlineData("customer1@teste.com", "002", 2)]
        //public void AddOrderWithItems(string email, string productCode, int quantity)
        //{
        //    //arrange (given)
        //    var product = _products
        //    .Where(p => p.Code == productCode)
        //    .SingleOrDefault();

        //    var customer = _customers
        //        .Where(i => i.Email == email)
        //        .SingleOrDefault();

        //    //Act (when)
        //    var order = GetOrder(customer);
                        
        //    var orderItem = _orderItems
        //        .Where(i => i.Product.Code == productCode && i.Order.Id == order.Id)
        //        .SingleOrDefault();

        //    if (orderItem == null)
        //    {
        //        orderItem = new OrderItem(order, product, quantity, product.Price);
        //        _orderItems.Add(orderItem);
        //    }

        //    //Assert (then)
        //    Assert.Equal(true, _orders.Where(i => i.Items.Count > 0).Any());
        //}
               

        //[Theory]
        //[InlineData(1)]
        //public void CancelOrder(int OrderId)
        //{
        //    //arrange
        //    var order = _orders.Where(i => i.Id == OrderId).SingleOrDefault();
        //    var count = _orders.Count();
        //    //act
        //    _orders.Remove(order);

        //    //assert
        //    Assert.Equal(count - 1, _orders.Count());
        //}

        //public Order GetOrder(Customer customer)
        //{
        //    var order = new Order(customer);
        //    _orders.Add(order);
                       
        //    return order;
        //}
    }
}