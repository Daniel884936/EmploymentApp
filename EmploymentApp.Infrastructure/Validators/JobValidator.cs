using EmploymentApp.Core.DTOs.JobDtos;
using FluentValidation;

namespace EmploymentApp.Infrastructure.Validators
{
    public class JobValidator : AbstractValidator<JobDto>
    {
        public JobValidator()
        {
            RuleFor(job => job.Company)
                .MaximumLength(80);

            RuleFor(job => job.Description)
                .NotNull();

            RuleFor(job => job.Title)
                .NotNull()
                .Length(10,60);

            RuleFor(job => job.CategoryId)
                .NotNull();

            RuleFor(job => job.StatusId)
                .NotNull();

            RuleFor(job => job.TypeScheduleId)
                .NotNull();

            RuleFor(job => job.UserId)
                .NotNull();
        }
    }
}
