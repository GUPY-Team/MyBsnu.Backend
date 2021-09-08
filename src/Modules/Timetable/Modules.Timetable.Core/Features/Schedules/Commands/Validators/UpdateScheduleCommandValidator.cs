using System;
using FluentValidation;

namespace Modules.Timetable.Core.Features.Schedules.Commands.Validators
{
    public class UpdateScheduleCommandValidator : AbstractValidator<UpdateScheduleCommand>
    {
        public UpdateScheduleCommandValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0);
            RuleFor(c => c.Year).InclusiveBetween(DateTime.UtcNow.Year, DateTime.UtcNow.Year + 1);
            RuleFor(c => c.Semester).NotEmpty();
        }
    }
}