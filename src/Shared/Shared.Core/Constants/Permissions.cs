using System.Collections.Generic;

namespace Shared.Core.Constants
{
    public static class Permissions
    {
        public const string CanManageAudiences = "Permissions.CanManageAudiences";
        public const string CanManageClasses = "Permissions.CanManageClasses";
        public const string CanManageCourses = "Permissions.CanManageCourses";
        public const string CanManageGroups = "Permissions.CanManageGroups";
        public const string CanManageSchedule = "Permissions.CanManageSchedule";
        public const string CanManageTeachers = "Permissions.CanManageTeachers";

        public const string SuperAdmin = "Permissions.SuperAdmin";

        public const string PermissionsPrefix = "Permissions";
        public const string PermissionsClaimType = "permission";

        public static readonly IReadOnlySet<string> All =
            new HashSet<string>
            {
                CanManageAudiences,
                CanManageClasses,
                CanManageCourses,
                CanManageGroups,
                CanManageSchedule,
                CanManageTeachers,
                SuperAdmin
            };
    }
}