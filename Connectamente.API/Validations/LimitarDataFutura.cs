using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.Validations;

public class LimitarDataFutura : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateOnly dataNascimento)
        {
            if (dataNascimento > DateOnly.FromDateTime(DateTime.Now))
            {
                return new ValidationResult("A data de nascimento não pode ser uma data futura.");
            }
        }

        return ValidationResult.Success;
    }
}