using FluentValidation;

namespace Modules.Timetable.Core.Features.Groups.Commands.Validators
{
    public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupCommandValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0);
            RuleFor(c => c.Number).NotEmpty().MaximumLength(256);
        }
    }
}