using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyInventorySystem.Data;
using PropertyInventorySystem.Models;
using PropertyInventorySystem.Repositories;
using System.Diagnostics;

namespace PropertyInventorySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoldPropertyController : ControllerBase
    {
        private readonly ISoldPropertyRepository _repository;
        private readonly AppDbContext _context;

        public SoldPropertyController(ISoldPropertyRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Create(SoldProperty soldProperty)
        {
            try
            {
                await _repository.AddAsync(soldProperty);
                return CreatedAtAction(nameof(GetById), new { propertyId = soldProperty.PropertyId, contactId = soldProperty.ContactId }, soldProperty);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SoldProperty>>> GetAll()
        {
            try
            {
                var soldProperties = await _repository.GetAllAsync();
                return Ok(soldProperties);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{propertyId}/{contactId}")]
        public async Task<ActionResult<SoldProperty>> GetById(Guid propertyId, Guid contactId)
        {
            try
            {
                var soldProperty = await _repository.GetByIdAsync(propertyId, contactId);
                if (soldProperty == null)
                {
                    return NotFound();
                }
                return Ok(soldProperty);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }
      

        [HttpPut("{propertyId}/{contactId}")]
        public async Task<ActionResult> Update(Guid propertyId, Guid contactId, SoldProperty soldProperty)
        {
            if (propertyId != soldProperty.PropertyId || contactId != soldProperty.ContactId)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateAsync(soldProperty);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{propertyId}/{contactId}")]
        public async Task<ActionResult> Delete(Guid propertyId, Guid contactId)
        {
            try
            {
                await _repository.DeleteAsync(propertyId, contactId);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
