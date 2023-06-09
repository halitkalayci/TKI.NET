using Entities.DTOs.Brand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationResolvers.Brand
{
    public class BrandForAddDtoValidator : AbstractValidator<BrandForAddDto>
    {
        public BrandForAddDtoValidator()
        {
            RuleFor(i => i.Name).NotNull().NotEmpty().MinimumLength(3);
            RuleFor(i=>i.LogoBase64).NotEmpty().NotNull();
        }
    }
}
