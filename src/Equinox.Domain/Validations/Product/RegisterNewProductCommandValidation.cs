using Equinox.Domain.Commands;

namespace Equinox.Domain.Validations
{
    public class RegisterNewProductCommandValidation : ProductValidation<RegisterNewProductCommand>
    {
        public RegisterNewProductCommandValidation()
        {
            ValidateName();
        }
    }
}