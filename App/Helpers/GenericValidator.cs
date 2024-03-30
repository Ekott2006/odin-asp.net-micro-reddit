using System.ComponentModel.DataAnnotations;

namespace App.Helpers;

public class GenericValidator
{
        public static bool TryValidate(object obj, out ICollection<ValidationResult> results)  
        {  
            ValidationContext context = new(obj, serviceProvider: null, items: null);  
            results = new List<ValidationResult>();  
            return Validator.TryValidateObject(  
                obj, context, results,  
                validateAllProperties: true  
            );  
        }  
}