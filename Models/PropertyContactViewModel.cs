using System.ComponentModel.DataAnnotations;

namespace PropertyInventorySystem.Models
{
    public class PropertyContactViewModel
    {
        public Property Property { get; set; }
        public Contact Contact { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Property.Price < 0)
            {
                yield return new ValidationResult(
                    "Price cannot be negative.",
                    new[] { nameof(Property.Price) });
            }

                }
        public static (Property, Contact) MapToEntity(PropertyContactViewModel model)
        {
            var property = new Property
            {
               
                Name = model.Property.Name,
                Address = model.Property.Address,
                Price = model.Property.Price,
                DateOfRegistration=model.Property.DateOfRegistration
              
            };

            var contact = new Contact
            {
             
                FirstName = model.Contact.FirstName, 
                LastName = model.Contact.LastName,
                PhoneNumber= model.Contact.PhoneNumber,
                Email = model.Contact.Email

            };

            return (property, contact);
        }
    }
 
}