using FluentValidation;

namespace Modules.Timetable.Core.Features.Courses.Commands.Validators
{
    public class CreateCourseValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseValidator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
        }
    }
}