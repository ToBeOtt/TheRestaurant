﻿using Common.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TheRestaurant.Application.Interfaces;

namespace TheRestaurant.Common.Infrastructure.Repositories.Category
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly RestaurantDbContext _context;
        public CategoryRepository(RestaurantDbContext context)
        {
            _context = context;
        }


        //Get Category By Id
        public async Task<Domain.Entities.Menu.Category> GetAsync(int id)
        {
            var category = await _context.Set<Domain.Entities.Menu.Category>().FirstOrDefaultAsync(c => c.Id == id); 
            return category;
        }


        //Get All Categories
        public async Task<IReadOnlyList<Domain.Entities.Menu.Category>> GetAllAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }


        //Add New Category
        public async Task AddAsync(Domain.Entities.Menu.Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        //Update the Category
        public async Task UpdateAsync(Domain.Entities.Menu.Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
        }

        //Delete the Category
        public async Task DeleteAsync(Domain.Entities.Menu.Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await GetAsync(id);

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        //Save Change
        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }

        // Category Exists
        public bool Exists(int id)
        {
            return (_context.Categories?.Any(c => c.Id == id)).GetValueOrDefault();
        }
    }
}