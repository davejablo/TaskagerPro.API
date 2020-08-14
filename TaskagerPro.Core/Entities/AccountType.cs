using System;
using System.Collections.Generic;
using System.Text;
using TaskagerPro.Core.Identities;

namespace TaskagerPro.Core.Entities
{
    public class AccountType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}