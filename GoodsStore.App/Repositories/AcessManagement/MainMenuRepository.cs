using AutoMapper;
using EmailService;
using GoodsStore.App.Infra;
using GoodsStore.App.Models;
using GoodsStore.App.Models.AccessManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoodsStore.App.Repositories
{
    public class MainMenuRepository : BaseRepository<MainMenu>, IMainMenuRepository
    {
        public MainMenuRepository(DBContext context): base(context) {}

        public async Task AddOrEdit(MainMenu menu)
        {
            //Insert
            if (menu.Id == 0)
            {
                _dbSet.Add(menu);
                await _context.SaveChangesAsync();
            }
            //Update
            else
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw ex;
                }
            }


            
        }

        public async Task<IEnumerable<MainMenu>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<MainMenu?> GetById(int id)
        {
            return await _dbSet.Where(e => e.Id == id).SingleOrDefaultAsync();
        }

        private bool ModelExists(int id)
        {
            return _dbSet.Any(e => e.Id== id);
        }
    }
}
