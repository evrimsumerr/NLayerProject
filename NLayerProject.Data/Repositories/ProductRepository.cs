using Microsoft.EntityFrameworkCore;
using NLayerProject.Core.Entities;
using NLayerProject.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext _appDbContext { get => _dbContext as AppDbContext; }
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Product> GetWithCategoryByIdAsync(int productId)
        {
            return await _appDbContext.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == productId);
        }
    }
}
