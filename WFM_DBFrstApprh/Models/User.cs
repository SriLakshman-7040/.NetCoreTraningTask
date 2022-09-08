using System;
using System.Collections.Generic;

namespace WFM_DBFrstApprh.Models
{
    public partial class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
