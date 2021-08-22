using FluentValidation;

namespace Modules.Timetable.Core.Features.Groups.Commands.Validators
{
    public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupCommandValidator()
        {
            RuleFor(c => c.Number).NotEmpty().MaximumLength(256);
        }
    }
}