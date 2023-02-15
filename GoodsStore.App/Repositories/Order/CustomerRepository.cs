using GoodsStore.App.Infra;
using GoodsStore.App.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodsStore.App.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DBContext context) : base(context)
        {
        }

        public async Task<Customer> Update(int id, Customer customer)
        {
            var registerDB =
                await _dbSet.Where(c => c.Id == id)
                .SingleOrDefaultAsync();

            if (registerDB == null)
            {
                throw new ArgumentNullException("register");
            }

            registerDB.Update(customer);
            await _context.SaveChangesAsync();
            return registerDB;
        }
    }
}
