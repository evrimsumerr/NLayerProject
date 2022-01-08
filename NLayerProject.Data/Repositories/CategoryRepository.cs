using Microsoft.EntityFrameworkCore;
using NLayerProject.Core.Entities;
using NLayerProject.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AppDbContext _appDbContext { get => _dbContext as AppDbContext; }
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category> GetWithProductsByIdAsync(int categoryId)
        {
            return await _appDbContext.Categories.Include(x => x.Products).SingleOrDefaultAsync(x => x.Id == categoryId);
        }
    }
}
