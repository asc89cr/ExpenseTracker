using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExpenseTrackerWeb
{
    public static class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                return token.Claims;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
