using System.Collections.Generic;

namespace Shared.Core.Constants
{
    public static class Permissions
    {
        public const string ScheduleEditor = "Permissions.ScheduleEditor";

        public const string SuperAdmin = "Permissions.SuperAdmin";

        public const string PermissionsPrefix = "Permissions";
        public const string PermissionClaimType = "permission";

        public static readonly IReadOnlySet<string> All =
            new HashSet<string>
            {
                ScheduleEditor,
                SuperAdmin
            };
    }
}