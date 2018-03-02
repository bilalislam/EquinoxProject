using System;
using Equinox.Domain.Validations;

namespace Equinox.Domain.Commands
{
    public class RegisterNewProductCommand : ProductCommand
    {
        public RegisterNewProductCommand(string name)
        {
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}