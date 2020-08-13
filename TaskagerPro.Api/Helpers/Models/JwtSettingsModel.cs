using System;
using System.Collections.Generic;
using System.Text;

namespace TaskagerPro.Core.Models
{
    public class JwtSettingsModel
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireDays { get; set; }
    }
}
