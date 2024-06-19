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
            _context = context; // Assign the injected context to the private field
        }
      

        // Dependency injection for repositories or services if needed

        [HttpGet]
        public IActionResult Index()
        {
            var model = new PropertyContactViewModel();
            return View(model);
        }
        public IActionResult Create()
        {
            // Initialize your ViewModel if needed
            var viewModel = new PropertyContactViewModel();
            // Set any default values or load necessary data

            return View(viewModel); // Pass the ViewModel to the View
        }

        [HttpPost]
        public async Task<IActionResult> Create(PropertyContactViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                //    return View(model);
                return View("Dashboard");

            }
            foreach (var modelStateKey in ViewData.ModelState.Keys)
            {
                var modelStateVal = ViewData.ModelState[modelStateKey];
                if (modelStateVal.Errors != null) // Added null check for modelStateVal.Errors
                {
                    foreach (var error in modelStateVal.Errors)
                    {
                        var errorMessage = error.ErrorMessage;
                        // Log the error message or set a breakpoint here to inspect
                        _logger.LogError("Validation error for {ModelKey}: {ErrorMessage}", modelStateKey, errorMessage);
                    }
                }
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Assuming PropertyContactViewModel has Property and Contact objects
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
                        // Set EffectiveTill or other properties as needed
                    };
                    _context.SoldProperties.Add(soldProperty);
                    await _context.SaveChangesAsync();

                    // Commit transaction if all operations succeed
                    transaction.Commit();

                    TempData["SuccessMessage"] = "Property and Contact information saved successfully!";
                 //   return RedirectToAction(nameof(Index));
                    return RedirectToAction("Dashboard", "Home");
                }
                catch (Exception ex)
                {
                    // Log the error (uncomment ex variable name and write a log.)
                    _logger.LogError(ex, "An error occurred while saving the data.");
                    transaction.Rollback();
                    ModelState.AddModelError("", "An error occurred while saving the data.");
                }
                // Return the view with validation messages
                //   return View(model);
            }
            return RedirectToAction("Dashboard", "Home");
        }
    }
}
