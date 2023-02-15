using GoodsStore.App.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GoodsStore.App.Infra
{
    public class DataService : IDataService
    {
        private readonly DBContext _context;
        private readonly IProductRepository _productRepository;
        private IWebHostEnvironment _env;

        public DataService(DBContext context, IWebHostEnvironment env, IProductRepository productRepository)
        {
            this._context = context;
            this._productRepository = productRepository;
            this._env = env;
        }

        public async Task InicializaDB()
        {
            await _context.Database.MigrateAsync();

            //var file = File.ReadAllLines(Path.Combine(_env.WebRootPath + "\\data", "books.json"));

            List<ProductsByImport>? books = await GetProducts();
            await _productRepository.SaveProducts(books);
        }

        

        private async Task<List<ProductsByImport>> GetProducts()
        {
            var json = await File.ReadAllTextAsync(Path.Combine(_env.WebRootPath + "\\data", "books.json"));
            var books = JsonConvert.DeserializeObject<List<ProductsByImport>>(json);
            return books;
        }
    }

    
}

