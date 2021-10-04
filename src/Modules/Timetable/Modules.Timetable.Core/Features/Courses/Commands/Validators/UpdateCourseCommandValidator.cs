using FluentValidation;

namespace Modules.Timetable.Core.Features.Courses.Commands.Validators
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(256);
            RuleFor(c => c.ShortName).NotEmpty().MaximumLength(32);
        }
    }
}