using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PropertyInventorySystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PropertyInventorySystem.Data;
namespace PropertyInventorySystem.Repositories
{
    public class SoldPropertyRepository : ISoldPropertyRepository
    {
        private readonly AppDbContext _context;

        public SoldPropertyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SoldProperty>> GetAllAsync()
        {
            try
            {
                return await _context.SoldProperties
                    .Include(sp => sp.Property)
                    .Include(sp => sp.Contact)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                throw new Exception("Could not retrieve sold properties.", ex);
            }
        }

        public async Task<SoldProperty> GetByIdAsync(Guid propertyId, Guid contactId)
        {
            try
            {
                return await _context.SoldProperties
                    .Include(sp => sp.Property)
                    .Include(sp => sp.Contact)
                    .FirstOrDefaultAsync(sp => sp.PropertyId == propertyId && sp.ContactId == contactId);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                throw new Exception($"Could not retrieve sold property with Property ID {propertyId} and Contact ID {contactId}.", ex);
            }
        }

        public async Task AddAsync(SoldProperty soldProperty)
        {
            try
            {
                _context.SoldProperties.Add(soldProperty);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                throw new Exception("Could not add sold property.", ex);
            }
        }

        public async Task UpdateAsync(SoldProperty soldProperty)
        {
            try
            {
                _context.SoldProperties.Update(soldProperty);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                throw new Exception("Could not update sold property.", ex);
            }
        }

        public async Task DeleteAsync(Guid propertyId, Guid contactId)
        {
            try
            {
                var soldProperty = await _context.SoldProperties
                    .FirstOrDefaultAsync(sp => sp.PropertyId == propertyId && sp.ContactId == contactId);
                _context.SoldProperties.Remove(soldProperty);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                throw new Exception($"Could not delete sold property with Property ID {propertyId} and Contact ID {contactId}.", ex);
            }
        }
    }

}
