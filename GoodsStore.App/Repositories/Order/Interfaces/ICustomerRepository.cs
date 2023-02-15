using GoodsStore.App.Models;

namespace GoodsStore.App.Repositories
{
    public interface ICustomerRepository
    {        
        Task<Customer> Update(int id, Customer customer);
    }
}