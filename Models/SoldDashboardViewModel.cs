using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PropertyInventorySystem.Models
{
    public class SoldDashboardViewModel
    { 
        public Guid PropertyId { get; set; }
        public Guid ContactId { get; set; }
        public Property Property { get; set; }
        public Contact Contact { get; set; }

        public string PropertyName { get; set; }
        public decimal AskingPriceEUR { get; set; }
        public string Owner { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
      
        public DateTime DateOfPurchase { get; set; }
        public decimal SoldAtPriceEUR { get; set; }
     
        public decimal AcquisitionPrice { get; set; }    
       
        public DateTime EffectiveTill { get; set; }

    }
}