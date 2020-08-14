using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using TaskagerPro.Core.Entities;

namespace TaskagerPro.Core.Identities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AccountTypeId { get; set; }
        public AccountType AccountType { get; set; }
        public string Sex { get; set; }
    }
}
