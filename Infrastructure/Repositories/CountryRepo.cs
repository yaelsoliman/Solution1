using Application.Repositories;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CountryRepo : ICountryRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public CountryRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddNewasync(Country country)
        {
            await _dbContext.Countries.AddAsync(country);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Country country)
        {
            _dbContext.Countries.Remove(country);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<Country>> GetAllAsync()
        {
            return
                 await _dbContext.Countries.ToListAsync();
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            return await  _dbContext.Countries.Where(x=>x.Id == id).FirstOrDefaultAsync(); 
        }

        public async Task UpdateAsync(Country country)
        {
             _dbContext.Countries.Update(country);
            await _dbContext.SaveChangesAsync();
        }
    }
}
