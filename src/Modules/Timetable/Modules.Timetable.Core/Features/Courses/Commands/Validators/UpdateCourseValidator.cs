using FluentValidation;

namespace Modules.Timetable.Core.Features.Courses.Commands.Validators
{
    public class UpdateCourseValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
        }
    }
}