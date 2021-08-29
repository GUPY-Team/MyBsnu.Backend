﻿namespace Shared.DTO.Schedule
{
    public record ClassDto
    {
        public int Id { get; init; }
        public string Format { get; init; }
        public string Type { get; init; }
        public string WeekDay { get; init; }
        public string WeekType { get; init; }
        public string StartTime { get; init; }
        public string EndTime { get; init; }
        public string Duration { get; init; }
        public string CourseName { get; init; }
        public int CourseId { get; init; }
        public string TeacherName { get; init; }
        public int TeacherId { get; init; }
        public string AudienceNumber { get; init; }
        public int? AudienceId { get; init; }
        public int ScheduleId { get; init; }
    }
}