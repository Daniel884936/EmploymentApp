using EmploymentApp.Core.DTOs.UserDtos;
using FluentValidation;

namespace EmploymentApp.Infrastructure.Validators.UserValidators
{
    public class UserLoginValidator: AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator()
        {
            RuleFor(userLogin => userLogin.Email)
                .NotNull()
                .EmailAddress()
                .Length(6, 60);

            RuleFor(userLogin => userLogin.Password)
                .NotNull()
                .Length(8, 60);
        }
    }
}
