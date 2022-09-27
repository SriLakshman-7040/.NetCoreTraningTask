using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WFM.Domain_DbFrstApprh.Models;
using WFM_Api.Abstraction;
using WFM_Api.API_Context;

namespace WFM_Api.Services
{
    public class UserAuth : IUserAuth
    {
        private readonly WFM_API_Context _context;
        public UserAuth(WFM_API_Context context)
        {
            _context = context;
        }

        public Users_DBF GetUserByName(string userName)
        {
            return _context.Users.FirstOrDefault(x => x.Username == userName);
        }

        public AuthenticateResponse? Authenticate(AuthenticateRequest authenticateRequest)
        {
            var userExits = _context.Users.SingleOrDefault(u => u.Username == authenticateRequest.UserName && u.Password == authenticateRequest.Password);
            if (userExits == null) return null;

            var token = generateJwtToken(userExits);

            return new AuthenticateResponse(userExits, token);
        }

        private string generateJwtToken(Users_DBF userExits)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("WFM Application JWT Token Generator String");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim("UserName", userExits.Username.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
