namespace Modules.Timetable.Core.Constants
{
    public static class CacheKeys
    {
        public static string LatestGroupSchedule(int groupId) => $"latestGroupSchedule:{groupId}";
        public static string LatestTeacherSchedule(int teacherId) => $"latestTeacherSchedule:{teacherId}";
    }
}