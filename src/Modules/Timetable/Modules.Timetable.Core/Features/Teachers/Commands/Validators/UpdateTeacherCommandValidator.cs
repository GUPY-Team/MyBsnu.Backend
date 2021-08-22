using FluentValidation;

namespace Modules.Timetable.Core.Features.Teachers.Commands.Validators
{
    public class UpdateTeacherCommandValidator : AbstractValidator<UpdateTeacherCommand>
    {
        public UpdateTeacherCommandValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0);
            RuleFor(c => c.FirstName).NotEmpty().MaximumLength(256);
            RuleFor(c => c.LastName).NotEmpty().MaximumLength(256);
            RuleFor(c => c.ThirdName).NotEmpty().MaximumLength(256);
        }
    }
}