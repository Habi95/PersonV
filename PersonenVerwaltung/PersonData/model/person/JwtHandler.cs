using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PersonData.model.person
{
    public class JwtHandler
    {
        private const string Key = "DigitalCampusVorarlbergProjektVerwaltung";

        public static string GenerateToken(bool authentication)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, authentication.ToString()) }),
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Convert.FromBase64String(Key)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.WriteToken(tokenhandler.CreateToken(tokenDescriptor));
            return token;
        }
    }
}