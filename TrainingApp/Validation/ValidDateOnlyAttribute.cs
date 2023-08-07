using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Validation;

public class ValidDateOnlyAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateOnly date && date > DateOnly.MinValue)
        {
            return ValidationResult.Success;
        }
        return new ValidationResult("ScheduledDate must be a valid date.");
    }
}