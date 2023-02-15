using GoodsStore.App.Infra;
using GoodsStore.App.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodsStore.App.Repositories
{
    public abstract class BaseRepository<T> where T : BaseModel
    {
        protected readonly DBContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
    }
}
