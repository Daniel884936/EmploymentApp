using EmploymentApp.Core.DTOs.JobDtos;
using EmploymentApp.Infrastructure.Options;
using FluentValidation;
using Microsoft.Extensions.Options;
using System.Linq;

namespace EmploymentApp.Infrastructure.Validators
{
    public class JobValidator : AbstractValidator<JobDto>
    {
        private readonly FileOptions _fileOptions;
        public JobValidator(IOptions<FileOptions> options)
        {
            _fileOptions = options.Value;

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

            RulesForImg();
        }

        private void RulesForImg()
        {
            RuleFor(job => job.Img)
                .Must(img =>
                {
                    if (img != null)
                    {
                        if (_fileOptions.ValidTypes.Contains(img.ContentType)) return true;
                    }
                    return false;
                })
                .WithMessage($"must be {string.Join(", ",_fileOptions.ValidTypes)}");


            RuleFor(job => job.Img)
                .Must(img =>
                {
                    if (img != null)
                    {
                        int kbSize = _fileOptions.MaxKb;
                        if (img.Length / 1000 <= kbSize) return true;
                    }
                    return false;
                })
                .WithMessage($"max size {_fileOptions.MaxKb}kb");
        }
    }
}
