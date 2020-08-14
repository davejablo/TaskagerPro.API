using System;
using System.Collections.Generic;
using System.Text;

namespace TaskagerPro.Core.Models
{
    public class TokenModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AccountType { get; set; }
        public int AccountTypeId { get; set; }
    }
}