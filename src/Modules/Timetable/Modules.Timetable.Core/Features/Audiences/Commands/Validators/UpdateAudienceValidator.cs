using FluentValidation;

namespace Modules.Timetable.Core.Features.Audiences.Commands.Validators
{
    public class UpdateAudienceValidator : AbstractValidator<UpdateAudienceCommand>
    {
        public UpdateAudienceValidator()
        {
            RuleFor(a => a.Corps).GreaterThan(0);
            RuleFor(a => a.Floor).GreaterThan(0);
            RuleFor(a => a.Room).GreaterThan(0);
        }
    }
}