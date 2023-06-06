using Core.Entities.Concretes;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    public class JwtTokenHelper : ITokenHelper
    {
        private IConfiguration _configuration;
        private TokenOptions _tokenOptions;

        public JwtTokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenOptions = _configuration.GetSection("TokenOptions") as TokenOptions;
        }


        public AccessToken CreateToken(User user)
        {
            DateTime accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: accessTokenExpiration,
                signingCredentials: signInCredentials,
                notBefore: DateTime.UtcNow,
                claims: SetClaims(user)
                );

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            //TODO:fix
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken()
            {
                Token = token,
                Expiration = accessTokenExpiration
            };
        }

        private IEnumerable<Claim> SetClaims(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            return claims;
        }
    }
}
