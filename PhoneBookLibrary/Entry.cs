using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBookLibrary
{

    /// <summary>
    /// Custom validation for phone types
    /// </summary>
    public class CustomTypeValidation : ValidationAttribute
    {
        /// <summary>
        /// List of allowed number types.
        /// </summary>
        public List<string> AllowedTypes = new List<string>
        {
            "Work", "Cellphone", "Home"
        };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(AllowedTypes.Contains(value.ToString()))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Phone type can be Work, Cellphone or Home");
            }            
        }
    }
    
    
    [Serializable] // To write to binary file
    public class Entry
    {
        public string ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [CustomTypeValidation]
        public string Type { get; set; }

        [Required]
        public string Number { get; set; }
    }
}
