using Microsoft.AspNetCore.Mvc;
using PropertyInventorySystem.Models;
using PropertyInventorySystem.Repositories;
using System.Diagnostics;

namespace PropertyInventorySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _repository;

        public ContactController(IContactRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAll()
        {
            try
            {
                var contacts = await _repository.GetAllAsync();
                return Ok(contacts);
            }
            catch (Exception)
            {
                
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetById(Guid id)
        {
            try
            {
                var contact = await _repository.GetByIdAsync(id);
                if (contact == null)
                {
                    return NotFound();
                }
                return Ok(contact);
            }
            catch (Exception)
            {
               
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Contact contact)
        {
            try
            {
                await _repository.AddAsync(contact);
                return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact);
            }
            catch (Exception)
            {
               
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateAsync(contact);
                return NoContent();
            }
            catch (Exception)
            {
             
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
               
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
