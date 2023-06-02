using Entities.DTOs.Car;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationResolvers.Car
{
    public class AddCarDtoValidator : AbstractValidator<CarForAddDto>
    {
        public AddCarDtoValidator()
        {
            RuleFor(i => i.Plate).Length(6).NotNull().NotEmpty().WithMessage("Plaka geçersiz.");


            RuleFor(i => i.ModelYear).GreaterThan(DateTime.Now.Year-11).LessThan(DateTime.Now.Year+1);


            RuleFor(i => i.Plate).Must(StartWithAnkara);
        }

        private bool StartWithAnkara(string arg)
        {
            return arg.StartsWith("06");
        }
    }
}
