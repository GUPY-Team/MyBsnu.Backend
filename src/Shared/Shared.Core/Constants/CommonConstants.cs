namespace Shared.Core.Constants
{
    public class CommonConstants
    {
        public static class Formatting
        {
            public const string TimeSpan = @"hh\:mm";
        }
        
        public static class Regex
        {
            public const string Time = "^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
        }

        public static class Pagination
        {
            public const int MinPage = 1;
            public const int DefaultPage = 1;

            public const int MinPageSize = 1;
            public const int DefaultPageSize = 10;

            public const int DefaultMaxPageSize = 100;
            public const int ExtendedMaxPageSize = short.MaxValue;    
        }
    }
}