using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFM.Domain_DbFrstApprh.Models
{
    public class AuthenticateResponse
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(Users_DBF userDetail,string token)
        {
            UserName = userDetail.Username;
            Password = userDetail.Password;
            Email = userDetail.Email;
            Role = userDetail.Role;
            Name = userDetail.Name;
            Token = token;
        }
    }
}
