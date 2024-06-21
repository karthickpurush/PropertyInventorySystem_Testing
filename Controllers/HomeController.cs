using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyInventorySystem.Data;
using PropertyInventorySystem.Models;
using System.Diagnostics;

namespace PropertyInventorySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context; // DbContext

        public HomeController(ILogger<HomeController> logger, AppDbContext context) 
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard() // New Dashboard action
        {
            var soldProperties = await _context.SoldProperties
                .Include(sp => sp.Property)
                .Include(sp => sp.Contact)
                .ToListAsync();

            var dashboardData = soldProperties.Select(sp => new PropertyDashboardViewModel
            {
                PropertyName = sp.Property.Name,
                AskingPriceEUR = sp.Property.Price, // Assuming this is the asking price
                Owner = $"{sp.Contact.FirstName} {sp.Contact.LastName}",
                ContactNumber = sp.Contact.PhoneNumber,
                Email = sp.Contact.Email,
                PropertyAddress = sp.Property.Address,
                DateOfPurchase = sp.Property.DateOfRegistration, // Assuming this is the purchase date
                SoldAtPriceEUR = sp.AcquisitionPrice, // Assuming SoldProperty has this field
                SoldAtPriceUSD = ConvertEURtoUSD(sp.AcquisitionPrice) // Convert using below method 
            }).ToList();

            return View(dashboardData); // View at Views/Home/Dashboard.cshtml
        }
        private static decimal ConvertEURtoUSD(decimal amountEUR)
        {
            decimal exchangeRate = 1.10M; // Placeholder exchange rate / Future Task we can use API to get the exchange rate
            return amountEUR * exchangeRate;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
