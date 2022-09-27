using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WFM_Api.Abstraction;

namespace WFM_Api.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate requestDelegate,IOptions<AppSettings> appSetting)
        {
            _requestDelegate = requestDelegate;
            _appSettings = appSetting.Value;
        }
        public async Task Invoke(HttpContext context, IUserAuth userAuth)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, userAuth, token);
            await _requestDelegate(context);
        }
        private void attachUserToContext(HttpContext context, IUserAuth userAuth,string token)
        {
            try 
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("WFM Application JWT Token Generator String");
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userName = Convert.ToString(jwtToken.Claims.First(u => u.Type == "UserName").Value);

                // attach user to context on successful jwt validation
                context.Items["User"] = userAuth.GetUserByName(userName);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
