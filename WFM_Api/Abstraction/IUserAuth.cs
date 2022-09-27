using WFM.Domain_DbFrstApprh.Models;

namespace WFM_Api.Abstraction
{
    public interface IUserAuth
    {
        AuthenticateResponse? Authenticate(AuthenticateRequest authenticateRequest);
        public Users_DBF GetUserByName(string userName);
    }
}
