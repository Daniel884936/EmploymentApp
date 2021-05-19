using EmploymentApp.Core.DTOs.UserDtos;
using EmploymentApp.Infrastructure.Options;
using FluentValidation;
using Microsoft.Extensions.Options;
using System;

namespace EmploymentApp.Infrastructure.Validators.UserValidators
{
    public class UserValidator: AbstractValidator<UserDto>
    {
        private readonly FileOptions _fileOptions;

        public UserValidator(IOptions<FileOptions> options)
        {
            _fileOptions = options.Value;

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

            RuleFor(user => user.Img)
               .Must(img => FileValidator.ValidFileType(img, _fileOptions.ValidTypes))
               .WithMessage($"must be {string.Join(", ", _fileOptions.ValidTypes)}");

            RuleFor(user => user.Img)
                .Must(img => FileValidator.LessThan(img, _fileOptions.MaxKb))
                .WithMessage($"max size {_fileOptions.MaxKb}kb");
        }
    }
}
