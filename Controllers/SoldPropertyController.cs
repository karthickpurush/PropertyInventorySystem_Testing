using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyInventorySystem.Data;
using PropertyInventorySystem.Models;

namespace PropertyInventorySystem.Controllers
{
    public class SoldPropertyController : Controller
    {
        private readonly AppDbContext _context;

        public SoldPropertyController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                // Fetch sold properties including related Property and Contact data
                var soldProperties = await _context.SoldProperties
                .Include(sp => sp.Property)
                .Include(sp => sp.Contact)
                .ToListAsync();

                // Map to the ViewModel
                var dashboardData = soldProperties.Select(sp => new PropertyDashboardViewModel
                {
                    //  Id = sp.Property.Id,s
                    PropertyName = sp.Property.Name,
                    AskingPriceEUR = sp.Property.Price, // Assuming this is the asking price
                    Owner = $"{sp.Contact.FirstName} {sp.Contact.LastName}",
                    ContactNumber = sp.Contact.PhoneNumber,
                    Email = sp.Contact.Email,
                    PropertyAddress = sp.Property.Address,
                    DateOfPurchase = sp.Property.DateOfRegistration, // Assuming this is the purchase date
                    SoldAtPriceEUR = sp.AcquisitionPrice, // Assuming SoldProperty has this field
                    SoldAtPriceUSD = ConvertEURtoUSD(sp.AcquisitionPrice) // Convert using your method
                }).ToList();

                return View(dashboardData);
            }
            catch (Exception ex)
            {
                // Log the exception details
                // Consider using a logging framework or service
                Console.WriteLine(ex.ToString());

                // Optionally, return a custom error view
                // return View("Error");

                // Or return a generic error message to the user
                return Content("An error occurred while processing your request. Please try again later.");
            }
        }
        private decimal ConvertEURtoUSD(decimal amountEUR)
        {
            decimal exchangeRate = 1.10M; // Placeholder exchange rate
            return amountEUR * exchangeRate;
        }
    }
}
