using Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface ICountryRepo
    {
        Task AddNewasync(Country country);
        Task DeleteAsync(Country country);
        Task<List<Country>> GetAllAsync();
        Task UpdateAsync(Country country);
        Task<Country> GetByIdAsync(int id);
    }
}
