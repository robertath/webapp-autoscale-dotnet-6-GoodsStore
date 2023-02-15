using GoodsStore.App.Models;
using GoodsStore.App.Repositories;

namespace GoodsStore.App.Repositories
{
    public interface IProductRepository
    {
        Task SaveProducts(List<ProductsByImport>? books);

        Task<IList<Product>> GetProducts();
    }
}