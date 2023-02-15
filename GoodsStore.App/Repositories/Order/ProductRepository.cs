using GoodsStore.App.Infra;
using GoodsStore.App.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodsStore.App.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DBContext context) : base(context)
        {
        }

        public async Task<IList<Product>> GetProducts()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task SaveProducts(List<ProductsByImport>? books)
        {
            foreach (var item in books)
            {
                if (!await _dbSet.Where(i => i.Code == item.Code).AnyAsync())
                    await _dbSet.AddAsync(new Product(item.Code, item.Name, item.Price));
            }
            await _context.SaveChangesAsync();
        }
    }


    public class ProductsByImport
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
