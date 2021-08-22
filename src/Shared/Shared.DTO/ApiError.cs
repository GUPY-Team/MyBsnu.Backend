using System.Collections.Generic;

namespace Shared.DTO
{
    public class ApiError
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}