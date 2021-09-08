using System.Collections.Generic;

namespace Shared.DTO
{
    public class ApiError
    {
        public int Status { get; init; }
        public string Message { get; init; }
        public Dictionary<string, string[]> Errors { get; init; }
    }
}