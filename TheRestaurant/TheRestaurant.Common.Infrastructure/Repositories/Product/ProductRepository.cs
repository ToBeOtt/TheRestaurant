﻿using Common.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRestaurant.Application.Interfaces.IProduct;

namespace TheRestaurant.Common.Infrastructure.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly RestaurantDbContext _context;

        public ProductRepository(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Domain.Entities.Menu.Product menuItem)
        {
            _context.Products.Add(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            var itemToDelete = await _context.Products.FindAsync(id);
            if (itemToDelete != null)
            {
                itemToDelete.IsDeleted = true;
                _context.Products.Update(itemToDelete);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Error handling
            }
        }

        public async Task<List<Domain.Entities.Menu.Product>> GetAllAsync()
        {
            return await _context.Products.Where(item => !item.IsDeleted).ToListAsync();
        }

        public async Task<Domain.Entities.Menu.Product> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(Domain.Entities.Menu.Product menuItem)
        {
            _context.Update(menuItem);
            await _context.SaveChangesAsync();
        }
    }
}