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

            // Additional custom validation logic
        }
        public static (Property, Contact) MapToEntity(PropertyContactViewModel model)
        {
            var property = new Property
            {
                // Assuming your Property entity has Name and Address properties
                Name = model.Property.Name,
                Address = model.Property.Address,
                Price = model.Property.Price,
                DateOfRegistration=model.Property.DateOfRegistration

                // Map other properties as needed
            };

            var contact = new Contact
            {
                // Assuming your Contact entity has Name and Email properties
                FirstName = model.Contact.FirstName, 
                LastName = model.Contact.LastName,
                PhoneNumber= model.Contact.PhoneNumber,
                Email = model.Contact.Email

                // Map other properties as needed
            };

            return (property, contact);
        }
    }
 
}