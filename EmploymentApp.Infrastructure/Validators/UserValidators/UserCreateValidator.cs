using EmploymentApp.Core.DTOs.UserDtos;
using FluentValidation;
using System;

namespace EmploymentApp.Infrastructure.Validators.UserValidators
{
    public class UserCreateValidator: AbstractValidator<UserCreateDto>
    {
        public UserCreateValidator()
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

            RuleFor(user => user.Password)
                .NotNull()
                .Length(8, 60);

            RuleFor(user => user.Email)
               .NotNull()
               .EmailAddress()
               .Length(6, 60);

            RuleFor(user => user.RoleId)
              .NotNull();
        }

    }
}
