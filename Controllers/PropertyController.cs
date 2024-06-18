using Microsoft.AspNetCore.Mvc;
using PropertyInventorySystem.Models;
using PropertyInventorySystem.Repositories;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace PropertyInventorySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class PropertyController : ControllerBase
    {        
               
        private readonly IPropertyRepository _propertyRepository;
        private readonly IContactRepository _contactRepository;

        public PropertyController(IPropertyRepository propertyRepository, IContactRepository contactRepository)
        {
            _propertyRepository = propertyRepository;
            _contactRepository = contactRepository;
        }

        [HttpPost("create-with-contact")]
        public async Task<ActionResult> CreateWithContact(PropertyContactViewModel model)
        {
          
            try
            {
                // 1. Add the Contact First
                await _contactRepository.AddAsync(model.Contact);
                // 2. Create and add the SoldProperty
                var soldProperty = new SoldProperty
                {
                    Contact = model.Contact,
                    AcquisitionPrice = model.Property.Price,
                    DateOfRegistration = model.Property.DateOfRegistration
                };
                // 3. Add the SoldProperty to the Property
                model.Property.SoldProperties.Add(soldProperty);
                // 4. Add the Property to the database 
                await _propertyRepository.AddAsync(model.Property);
                // 5. Serialize the property object (now without a circular reference)
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                var propertyJson = JsonSerializer.Serialize(model.Property, options);
                // 6. Return the CreatedAtAction response
                return CreatedAtAction(nameof(GetById), new { id = model.Property.Id }, propertyJson);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Property>>> GetAll()
        {
            try
            {
                var properties = await _propertyRepository.GetAllAsync();
                return Ok(properties);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> GetById(Guid id)
        {
            try
            {
                var property = await _propertyRepository.GetByIdAsync(id);
                if (property == null)
                {
                    return NotFound();
                }
                return Ok(property);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Property property)
        {
            try
            {
                await _propertyRepository.AddAsync(property);
                return CreatedAtAction(nameof(GetById), new { id = property.Id }, property);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, Property property)
        {
            if (id != property.Id)
            {
                return BadRequest();
            }

            try
            {
                await _propertyRepository.UpdateAsync(property);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _propertyRepository.DeleteAsync(id);
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
