using GoodsStore.App.Models.AccessManagement;

namespace GoodsStore.App.Repositories
{
    public interface IMainMenuRepository
    {
        Task AddOrEdit(MainMenu menu);
        Task<IEnumerable<MainMenu>> GetAll();
        Task<MainMenu> GetById(int id);
    }
}