using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PropertyInventorySystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PropertyInventorySystem.Data;
namespace PropertyInventorySystem.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            try
            {
                return await _context.Contacts.ToListAsync();
            }
            catch (Exception ex)
            {
             
                throw new Exception("Could not retrieve contacts.", ex);
            }
        }

        public async Task<Contact> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Contacts.FindAsync(id);
            }
            catch (Exception ex)
            {
               
                throw new Exception($"Could not retrieve contact with ID {id}.", ex);
            }
        }

        public async Task AddAsync(Contact contact)
        {
            try
            {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
               
                throw new Exception("Could not add contact.", ex);
            }
        }

        public async Task UpdateAsync(Contact contact)
        {
            try
            {
                _context.Contacts.Update(contact);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
              
                throw new Exception("Could not update contact.", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var contact = await _context.Contacts.FindAsync(id);
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
             
                throw new Exception($"Could not delete contact with ID {id}.", ex);
            }
        }
    }

}
