using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PropertyInventorySystem.Models
{
   
    public class PropertyDashboardViewModel
    {
        public int Id { get; set; }
        public Guid PropertyId { get; set; }
        public Guid ContactId { get; set; }
        public Property Property { get; set; }
        public Contact Contact { get; set; }

        public string PropertyName { get; set; }
        public decimal AskingPriceEUR { get; set; }
        public string Owner { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string PropertyAddress { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public decimal SoldAtPriceEUR { get; set; }
        public decimal SoldAtPriceUSD { get; set; }
    }
}
