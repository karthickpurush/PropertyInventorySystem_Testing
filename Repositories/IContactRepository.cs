using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PropertyInventorySystem.Models;


namespace PropertyInventorySystem.Repositories
{

    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(Guid id);
        Task AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task DeleteAsync(Guid id);
    }
}
