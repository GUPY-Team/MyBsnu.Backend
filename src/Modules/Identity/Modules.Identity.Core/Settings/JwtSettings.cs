﻿namespace Modules.Identity.Core.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public int TokenExpirationInMinutes { get; set; }
    }
}