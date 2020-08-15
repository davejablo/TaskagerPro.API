using System;
using System.Collections.Generic;
using System.Text;

namespace TaskagerPro.Core.DTOs.Account
{
    public class AccountDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal HourWage { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public int AccountTypeId { get; set; }
    }
}
