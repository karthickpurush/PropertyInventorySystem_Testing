using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyInventorySystem.Data;
using PropertyInventorySystem.Models;


namespace PropertyInventorySystem.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _context;

        public PropertyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            try
            {
                return await _context.Properties.Include(p => p.SoldProperties).ToListAsync();
            }
            catch (Exception ex)
            {
            
                throw new Exception("Could not retrieve properties.", ex);
            }
        }

        public async Task<Property> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Properties.Include(p => p.SoldProperties).FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
             
                throw new Exception($"Could not retrieve property with ID {id}.", ex);
            }
        }

        public async Task AddAsync(Property property)
        {
            try
            {
                _context.Properties.Add(property);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
              
                throw new Exception("Could not add property.", ex);
            }
        }

        public async Task UpdateAsync(Property property)
        {
            try
            {
                _context.Properties.Update(property);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
               
                throw new Exception("Could not update property.", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var property = await _context.Properties.FindAsync(id);
                _context.Properties.Remove(property);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
              
                throw new Exception($"Could not delete property with ID {id}.", ex);
            }
        }
    }

}
