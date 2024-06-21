using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyInventorySystem.Data;
using PropertyInventorySystem.Models;
using Microsoft.Extensions.Logging;

namespace PropertyInventorySystem.Controllers
{
    public class SoldDashboardController : Controller
    {
        private readonly ILogger<SoldDashboardController> _logger;
        private readonly AppDbContext _context;
        public SoldDashboardController(ILogger<SoldDashboardController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
      
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            var pageSize = 10; 
            ViewData["CurrentFilter"] = searchString;
          
            var query = _context.SoldProperties.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(sp => sp.Property.Name.Contains(searchString)
                                          || sp.Contact.LastName.Contains(searchString));
            }
            
            var viewModelQuery = query.Select(sp => new SoldDashboardViewModel
            {
                // Map properties from SoldProperty to SoldDashboardViewModel
                PropertyId = sp.Property.Id,
                ContactId = sp.Contact.Id,
                AskingPriceEUR = sp.Property.Price,
                Owner = $"{sp.Contact.FirstName} {sp.Contact.LastName}",
                ContactNumber = sp.Contact.PhoneNumber,
                Email = sp.Contact.Email,
                DateOfPurchase = sp.Property.DateOfRegistration,
                EffectiveTill = sp.EffectiveTill ?? DateTime.Now, // Assuming EffectiveTill is nullable
                SoldAtPriceEUR = sp.AcquisitionPrice,
               
            });

            // Create a PaginatedList<SoldDashboardViewModel> for the page number 
            var model = await PaginatedList<SoldDashboardViewModel>.CreateAsync(viewModelQuery.AsNoTracking(), pageNumber ?? 1, pageSize);           
          return View(model);
        }


        [HttpGet("View/{propertyId}/{contactId}")]
        public async Task<IActionResult> View(Guid propertyId, Guid contactId)
        {

            if (propertyId == Guid.Empty || contactId == Guid.Empty)
            {
                return NotFound();
            }

            // Assuming SoldProperties has a navigation property to Property and Contact
            var soldProperty = await _context.SoldProperties
                                              .Include(sp => sp.Property)
                                              .Include(sp => sp.Contact)
                                              .FirstOrDefaultAsync(sp => sp.PropertyId == propertyId && sp.ContactId == contactId);

            if (soldProperty == null || soldProperty.Property == null)
            {
                return NotFound();
            }

            // Map to the ViewModel
            var viewModel = new SoldDashboardViewModel
            {
                PropertyId = soldProperty.PropertyId,
                ContactId = soldProperty.ContactId,
                AskingPriceEUR = soldProperty.Property.Price,
                Owner = $"{soldProperty.Contact.FirstName} {soldProperty.Contact.LastName}",
                ContactNumber = soldProperty.Contact.PhoneNumber,
                Email = soldProperty.Contact.Email,
                DateOfPurchase = soldProperty.Property.DateOfRegistration,
                EffectiveTill = soldProperty.EffectiveTill ?? DateTime.Now, // Assuming EffectiveTill is nullable
                SoldAtPriceEUR = soldProperty.AcquisitionPrice,
               
            };

            return View(viewModel);
        }
      
        [HttpGet("Edit/{propertyId}/{contactId}")]
        public async Task<IActionResult> Edit(Guid propertyId, Guid contactId)
        {
           
            var soldProperty = await _context.SoldProperties
            .Include(sp => sp.Property)
            .Include(sp => sp.Contact)
              .FirstOrDefaultAsync(sp => sp.PropertyId == propertyId && sp.ContactId == contactId);
            
            if (soldProperty == null)
            {
                return NotFound();
            }

            // Map to the ViewModel
            var viewModel = new SoldDashboardViewModel
            {
                PropertyId = soldProperty.Property.Id,
                ContactId = soldProperty.Contact.Id,
                AskingPriceEUR = soldProperty.Property.Price,
                Owner = $"{soldProperty.Contact.FirstName} {soldProperty.Contact.LastName}",
                ContactNumber = soldProperty.Contact.PhoneNumber,
                Email = soldProperty.Contact.Email,
                DateOfPurchase = soldProperty.Property.DateOfRegistration,
                EffectiveTill = soldProperty.EffectiveTill ?? DateTime.Now, // Assuming EffectiveTill is nullable
                SoldAtPriceEUR = soldProperty.AcquisitionPrice,
            };

            return View(viewModel);
        }
        

        [HttpPost("Edit/{propertyId}/{contactId}")]
        public async Task<IActionResult> Edit(Guid propertyId, Guid contactId, SoldDashboardViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    // If ModelState is not valid, return the view with the validation errors
            //    return BadRequest(ModelState);
            //} Time being commented out and not using this validation.

          
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Retrieve existing property and contact if they exist
                    var propertyToUpdate = await _context.Properties.FindAsync(propertyId);
                    var contactToUpdate = await _context.Contacts.FindAsync(contactId);

                    if (propertyToUpdate == null || contactToUpdate == null)
                    {
                        ModelState.AddModelError("", "Property or Contact not found.");
                        return View(model);
                    }

                    // Update property and contact details from the model
                    propertyToUpdate.Price = model.AskingPriceEUR;
                    propertyToUpdate.DateOfRegistration = model.DateOfPurchase;

                    contactToUpdate.PhoneNumber = model.ContactNumber;
                    contactToUpdate.Email = model.Email;
                    var ownerParts = model.Owner.Split(' ');
                    contactToUpdate.FirstName = ownerParts.Length > 0 ? ownerParts[0] : contactToUpdate.FirstName;
                    contactToUpdate.LastName = ownerParts.Length > 1 ? ownerParts[1] : contactToUpdate.LastName;

                    await _context.SaveChangesAsync();

                    // Retrieve existing sold property
                    var soldPropertyToUpdate = await _context.SoldProperties
                        .FirstOrDefaultAsync(sp => sp.PropertyId == propertyId && sp.ContactId == contactId);

                    if (soldPropertyToUpdate == null)
                    {
                        ModelState.AddModelError("", "Sold Property not found.");
                        return View(model);
                    }

                    // Update sold property details
                    soldPropertyToUpdate.AcquisitionPrice = model.SoldAtPriceEUR;
                    soldPropertyToUpdate.DateOfRegistration = propertyToUpdate.DateOfRegistration;
                    soldPropertyToUpdate.EffectiveTill = model.EffectiveTill;

                  
                    // _context.SoldProperties.Update(soldPropertyToUpdate);

                    await _context.SaveChangesAsync();
                 
                    await transaction.CommitAsync();


                    TempData["SuccessMessage"] = "Sold Property information updated successfully!";
                    //  return RedirectToAction("Dashboard", "Home");
                }
                catch (Exception ex)
                {
                  
                    _logger.LogError(ex, "An error occurred while saving the data.");
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "An error occurred while saving the data.");                  
                    return View(model);
                }
            }
           
            return RedirectToAction("Dashboard", "Home");

        }


        [HttpPost("Delete/{propertyId}/{contactId}")]
        public async Task<IActionResult> Delete(Guid propertyId, Guid contactId)
        {

            if (propertyId == Guid.Empty || contactId == Guid.Empty)
            {
                return NotFound();
            }

            var soldProperty = await _context.SoldProperties.FindAsync(propertyId, contactId);
            if (soldProperty == null)
            {
                return NotFound();
            }

            try
            {
             
                _context.SoldProperties.Remove(soldProperty);
                await _context.SaveChangesAsync();             
                var property = await _context.Properties.FindAsync(soldProperty.PropertyId);
                if (property != null)
                {
                    _context.Properties.Remove(property);
                    await _context.SaveChangesAsync();
                }
              
                var contact = await _context.Contacts.FindAsync(soldProperty.ContactId);
                if (contact != null)
                {
                    _context.Contacts.Remove(contact);
                    await _context.SaveChangesAsync();
                }
               
                TempData["SuccessMessage"] = "Sold Property  deleted successfully!";
                return RedirectToAction(nameof(Index)); // Assuming you have an Index action to redirect to
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while deleting the record.");
                // Handle the error appropriately
                TempData["ErrorMessage"] = "An error occurred while deleting the record.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid propertyId)
        {
            var soldProperty = await _context.SoldProperties.FindAsync(propertyId);
            if (soldProperty != null)
            {
                _context.SoldProperties.Remove(soldProperty);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Sold Property deleted successfully!";
            }
            return RedirectToAction("Dashboard", "Home");
        }
    }
}
