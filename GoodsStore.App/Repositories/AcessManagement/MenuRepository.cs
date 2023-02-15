using AutoMapper;
using EmailService;
using GoodsStore.App.Infra;
using GoodsStore.App.Models;
using GoodsStore.App.Models.AccessManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoodsStore.App.Repositories
{
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public MenuRepository(DBContext context): base(context) {}

        public async Task AddOrEdit(Menu menu)
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

        public async Task<IList<Menu>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Menu?> GetById(int id)
        {
            return await _dbSet.Where(e => e.Id == id).SingleOrDefaultAsync();
        }

        private bool ModelExists(int id)
        {
            return _dbSet.Any(e => e.Id== id);
        }
    }
}
