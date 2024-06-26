using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PropertyInventorySystem.Models
{
    public class SoldProperty
    {
     
        public Guid PropertyId { get; set; }  
        public Property Property { get; set; }
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
        public decimal AcquisitionPrice { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public DateTime? EffectiveTill { get; set; }
     
    }
}
