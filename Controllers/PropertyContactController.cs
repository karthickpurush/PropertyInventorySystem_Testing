using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyInventorySystem.Data;
using PropertyInventorySystem.Models;
using Microsoft.Extensions.Logging;

namespace PropertyInventorySystem.Controllers
{
    public class PropertyContactController : Controller
    {
        private readonly ILogger<PropertyContactController> _logger;
        private readonly AppDbContext _context;
        public PropertyContactController(ILogger<PropertyContactController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        // Dependency injection for repositories if needed

        [HttpGet]
        public IActionResult Index()
        {
            var model = new PropertyContactViewModel();
            return View(model);
        }
        public IActionResult Create()
        {

            var viewModel = new PropertyContactViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PropertyContactViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                  
                    var property = model.Property;
                    var contact = model.Contact;

                    // Save Property
                    _context.Properties.Add(property);
                    await _context.SaveChangesAsync();

                    // Save Contact
                    _context.Contacts.Add(contact);
                    await _context.SaveChangesAsync();

                    // Create and Save SoldProperty
                    var soldProperty = new SoldProperty
                    {
                        PropertyId = property.Id,
                        ContactId = contact.Id,
                        AcquisitionPrice = property.Price,
                        DateOfRegistration = property.DateOfRegistration,
                      
                    };
                    _context.SoldProperties.Add(soldProperty);
                    await _context.SaveChangesAsync();
                
                    transaction.Commit();

                    TempData["SuccessMessage"] = "Property and Contact information saved successfully!";
              
                    return RedirectToAction("Dashboard", "Home");
                }
                catch (Exception ex)
                {
                 
                    _logger.LogError(ex, "An error occurred while saving the data.");
                    transaction.Rollback();
                    ModelState.AddModelError("", "An error occurred while saving the data.");
                }

                }           

            }
            foreach (var modelStateKey in ViewData.ModelState.Keys)
            {
                var modelStateVal = ViewData.ModelState[modelStateKey];
                if (modelStateVal.Errors != null) 
                {
                    foreach (var error in modelStateVal.Errors)
                    {
                        var errorMessage = error.ErrorMessage;
                      
                        _logger.LogError("Validation error for {ModelKey}: {ErrorMessage}", modelStateKey, errorMessage);
                    }
                }
            }
          
           return RedirectToAction("Dashboard", "Home");
        }
    }
}
