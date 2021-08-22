using System;

namespace Shared.DTO
{
    public record TimeDto
    {
        public int Hours { get; init; }
        public int Minutes { get; init; }

        public TimeSpan AsTimeSpan() => new(Hours, Minutes, 0);
    }
}