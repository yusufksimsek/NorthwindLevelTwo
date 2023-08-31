using FluentValidation;
using MvcWebUI.Models;

namespace MvcWebUI.ValidationRules.FluentValidation
{
    public class ShippingDetailValidator : AbstractValidator<ShippingDetail>
    {
        public ShippingDetailValidator()
        {
            RuleFor(s => s.FirstName).NotEmpty().WithMessage("Name is required");
            RuleFor(s => s.FirstName).MinimumLength(2);
            RuleFor(s => s.Address).NotEmpty();
            RuleFor(s => s.City).NotEmpty().When(s => s.Age < 18);

            RuleFor(s => s.FirstName).Must(startWithA);
        }

        private bool startWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
