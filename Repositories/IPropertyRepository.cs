using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PropertyInventorySystem.Models;


namespace PropertyInventorySystem.Repositories
{

    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllAsync();
        Task<Property> GetByIdAsync(Guid id);
        Task AddAsync(Property property);
        Task UpdateAsync(Property property);
        Task DeleteAsync(Guid id);
    }
}
