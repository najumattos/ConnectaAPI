using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.Validations;

public class IdadeMinima : ValidationAttribute
{
    private readonly int _idadeMinima;
    public IdadeMinima(int idadeMinima)
    {
        _idadeMinima = idadeMinima;
        // Mensagem de erro padrão caso você esqueça de definir uma
        ErrorMessage = $"A idade mínima permitida é de {_idadeMinima} anos.";
    }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateOnly dataNasc)
        {
            var hoje = DateOnly.FromDateTime(DateTime.Now);
            var idade = hoje.Year - dataNasc.Year;

            // Se o aniversário ainda não aconteceu este ano, subtrai 1 da idade
            if (dataNasc > hoje.AddYears(-idade))
            {
                idade--;
            }

            if (idade < _idadeMinima)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        return new ValidationResult("Data de nascimento inválida.");
    }
}