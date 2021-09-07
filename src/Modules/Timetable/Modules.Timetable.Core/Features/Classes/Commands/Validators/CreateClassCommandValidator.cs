﻿using FluentValidation;

namespace Modules.Timetable.Core.Features.Classes.Commands.Validators
{
    public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
    {
        public CreateClassCommandValidator()
        {
            RuleFor(c => c.Format).NotEmpty();
            RuleFor(c => c.Type).NotEmpty();
            RuleFor(c => c.WeekDay).NotEmpty();
            RuleFor(c => c.StartTime.AsTimeSpan()).LessThan(c => c.EndTime.AsTimeSpan());
            RuleFor(c => c.EndTime.AsTimeSpan()).GreaterThan(c => c.StartTime.AsTimeSpan());
            RuleFor(c => c.CourseId).GreaterThan(0);
            RuleFor(c => c.Teachers).NotEmpty();
            RuleFor(c => c.Groups).NotEmpty();
            RuleFor(c => c.ScheduleId).GreaterThan(0);
        }
    }
}