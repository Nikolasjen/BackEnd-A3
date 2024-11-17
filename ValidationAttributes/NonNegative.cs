using System.ComponentModel.DataAnnotations;


public class NonNegativePriceAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success!; // Consider null values as valid; handle them separately if needed... I'm lazy.
        }

        if (decimal.TryParse(value.ToString(), out decimal price))
        {
            if (price < 0)
            {
                return new ValidationResult("The price must be a non-negative value.");
            }

            return ValidationResult.Success!;
        }

        return new ValidationResult("Invalid price format.");
    }
}
