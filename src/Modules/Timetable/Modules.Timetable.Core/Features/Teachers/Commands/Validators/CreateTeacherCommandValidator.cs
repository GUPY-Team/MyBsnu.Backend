using FluentValidation;

namespace Modules.Timetable.Core.Features.Teachers.Commands.Validators
{
    public class CreateTeacherCommandValidator : AbstractValidator<CreateTeacherCommand>
    {
        public CreateTeacherCommandValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().MaximumLength(256);
            RuleFor(c => c.LastName).NotEmpty().MaximumLength(256);
            RuleFor(c => c.Patronymic).NotEmpty().MaximumLength(256);
        }
    }
}