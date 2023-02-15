using GoodsStore.App.Models.AccessManagement;

namespace GoodsStore.App.Repositories
{
    public interface IMenuRepository
    {
        Task AddOrEdit(Menu menu);
        Task<IList<Menu>> GetAll();
        Task<Menu> GetById(int id);
    }
}