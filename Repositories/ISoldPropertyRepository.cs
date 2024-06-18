using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PropertyInventorySystem.Models;


namespace PropertyInventorySystem.Repositories
{

    public interface ISoldPropertyRepository
    {
        Task<IEnumerable<SoldProperty>> GetAllAsync();
        Task<SoldProperty> GetByIdAsync(Guid propertyId, Guid contactId);
        Task AddAsync(SoldProperty soldProperty);
        Task UpdateAsync(SoldProperty soldProperty);
        Task DeleteAsync(Guid propertyId, Guid contactId);
    }
}
