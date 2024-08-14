using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace UsersApi.Service
{
    public class TokenService 
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;

            // Ensure the signing key is at least 512 bits (64 bytes)
            var signingKey = _config["JWT:SigningKey"];
            if (string.IsNullOrWhiteSpace(signingKey) || signingKey.Length < 64)
            {
                throw new ArgumentException("The JWT signing key must be at least 512 bits (64 bytes) in length.");
            }
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        }

       public string CreateToken(User user, IList<string> roles)
{
    var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
    };

    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

    var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(7),
        SigningCredentials = creds,
        Issuer = _config["JWT:Issuer"],
        Audience = _config["JWT:Audience"]
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
}
 }
}
