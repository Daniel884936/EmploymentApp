using EmploymentApp.Core.DTOs.CategoryDto;
using FluentValidation;

namespace EmploymentApp.Infrastructure.Validators
{
    public class CategoryValidator: AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.Name)
                .NotNull()
                .Length(2, 60);
        }
    }
}
