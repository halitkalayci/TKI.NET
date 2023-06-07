using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAttribute : MethodInterception
    {
        // OnBefore 
        // Validator type?
        private Type _type;

        public ValidationAttribute(Type type)
        {
            // Verilen type bir validator mu?
            if (!typeof(IValidator).IsAssignableFrom(type))
                throw new Exception("Verilen tip düzgün bir validatör değil!");
            _type = type;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            // verilen tipten bir consturctor üret. runtime
            // Runtime'da bir instance üretme
            var validator = (IValidator)Activator.CreateInstance(_type);
            var entityType = _type.BaseType.GetGenericArguments()[0];
            // Validaton işlemini yapacağım methodun entityType ile uyuşan parametlerinin hepsini valide et..
            var entities = invocation.Arguments.Where(i=> i.GetType() == entityType).ToList();
            foreach (var entity in entities)
            {
                var context = new ValidationContext<object>(entity);
                var result = validator.Validate(context);
                if (!result.IsValid)
                    throw new ValidationException(result.Errors);
            }
        }
    }
}
