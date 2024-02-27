using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Model;

namespace UserService
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "yPKCqn4kswLtaJwXvN2jGzpQRyTZ3gdXkt7FeBJP";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;
        private readonly List<UserAccounts> _userAccounts;

        public JwtTokenHandler()
        {
            _userAccounts = new List<UserAccounts>
            {
                new UserAccounts {UserName="admin",Password="admin123",Role="Administrator"},
                new UserAccounts {UserName="user01",Password="user01",Role="User"},
            };
        }

        //Creating a method for generating Jwt Token

        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrWhiteSpace(authenticationRequest.UserName) || string.IsNullOrWhiteSpace(authenticationRequest.Password))
            {
                return null;
            }

            //validation
            var userAccount = _userAccounts.Where(x => x.UserName == authenticationRequest.UserName && x.Password == authenticationRequest.Password).FirstOrDefault();
            if (userAccount == null)
            {
                return null;
            }

            //jwt token expiry time stamp

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);

            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            //claims identity object
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name,authenticationRequest.UserName),
                new Claim("Role",userAccount.Role),
            });

            //creating a singing credential object
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                 SecurityAlgorithms.HmacSha256Signature
                );


            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            //create jwt security token handler object

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            //convert token to string
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                UserName = userAccount.UserName,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token,
            };

        }
    }
}
