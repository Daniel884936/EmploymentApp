using EmploymentApp.Core.DTOs.JobDtos;
using FluentValidation;
using System.Linq;

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

            RuleFor(job => job.Img)
                .Must(img =>
                {
                    if (img != null)
                    {
                        string[] validTypes = { "image/png", "image/jpg", "image/gif" };
                        if (validTypes.Contains(img.ContentType)) return true;
                    }
                    return false;
                })
                .WithMessage("must be png, jpg or gif");


            RuleFor(job => job.Img)
                .Must(img =>
                {
                    if (img != null)
                    {
                        int kbSize = 100;
                        if (img.Length / 1000 <= kbSize) return true;

                    }
                    return false;
                })
                .WithMessage("max size 100kb");
        }
    }
}
