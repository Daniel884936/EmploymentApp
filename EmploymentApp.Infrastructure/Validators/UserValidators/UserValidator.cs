using EmploymentApp.Core.DTOs.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmploymentApp.Infrastructure.Validators.UserValidators
{
    public class UserValidator: AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name)
               .NotNull()
               .Length(3, 50);

            RuleFor(user => user.Surnames)
                .NotNull()
                .Length(3, 50);

            RuleFor(user => user.Bithdate)
               .NotNull()
               .LessThan(DateTime.Now)
               .GreaterThan(new DateTime(1900, 1, 1));
        }
    }
}
