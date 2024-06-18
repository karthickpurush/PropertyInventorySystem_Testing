using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyInventorySystem.Data;
using PropertyInventorySystem.Models;
namespace PropertyInventorySystem.Controllers
{
    public class PropertyContactController : Controller
    {
        private readonly AppDbContext _context;

        public PropertyContactController(AppDbContext context)
        {
            _context = context; // Assign the injected context to the private field
        }

        // Dependency injection for repositories or services if needed

       [HttpGet]
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
                try
                {
                    // Assuming you have a method to map your ViewModel to your Entity model
                    var (property, contact) = PropertyContactViewModel.MapToEntity(model);
                    _context.Properties.Add(property);
                    _context.Contacts.Add(contact);

                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Property and contact information saved successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Log the exception details
                    // This can provide insights into issues like constraint violations
                    // Example: _logger.LogError(ex, "An error occurred while saving the data.");
                    ModelState.AddModelError("", "An error occurred while saving the data.");
                }
                catch (Exception ex)
                {
                    // Log the exception and handle it
                    // Example: _logger.LogError(ex, "An unexpected error occurred.");
                    ModelState.AddModelError("", "An unexpected error occurred.");
                }
            }

            // If we got this far, something failed; redisplay the form
            // This includes handling model state errors and exceptions
            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    // Log or debug the error message
                    var errorMessage = error.ErrorMessage;
                    // Example: _logger.LogError("Model state error: {ErrorMessage}", errorMessage);
                }
            }
            return View(model);
        }

    }
}
