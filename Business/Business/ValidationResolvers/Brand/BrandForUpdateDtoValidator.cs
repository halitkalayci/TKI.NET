using Entities.DTOs.Brand;
using FluentValidation;

namespace Business.ValidationResolvers.Brand
{
    public class BrandForUpdateDtoValidator : AbstractValidator<BrandForUpdateDto>
    {
        public BrandForUpdateDtoValidator()
        {
            RuleFor(i => i.Id).NotNull().Must(i => i > 0);
            RuleFor(i => i.Name).NotNull().NotEmpty().MinimumLength(3);
        }
    }
}
