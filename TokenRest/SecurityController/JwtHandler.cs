using Microsoft.IdentityModel.Tokens;
using SecurityData.model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SecurityController
{
    public class JwtHandler
    {
        private const string Key = "DigitalCampusVorarlbergProjektVerwaltung";

        public static string GenerateToken(User user, string email)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim( ClaimTypes.Email,email),
                      new Claim( ClaimTypes.Role , user.admin.ToString()),
                       new Claim( ClaimTypes.Actor ,user.authentication.ToString() )}),
                Expires = DateTime.Now.AddMinutes(60).ToLocalTime(),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Convert.FromBase64String(Key)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.WriteToken(tokenhandler.CreateToken(tokenDescriptor));
            return token;
        }

        public static Token ReadToken(string token)
        {
            try
            {
                var t = token.Split(" ");
                var tokenhandler = new JwtSecurityTokenHandler();
                var jToken = tokenhandler.ReadJwtToken(t[1]);
                var tokenObject = CreateToken(jToken.Payload.Claims.ToList());

                if (tokenObject.expDate > DateTime.Now)
                {
                    return tokenObject;
                }
                else
                {
                    throw new Exception("Bitte neu Anmelden Session Abgelaufen");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Token CreateToken(List<Claim> claims)
        {
            var email = claims[0].ToString();
            var emailA = email.Split(" ");
            var role = claims[1].ToString();
            var roleA = role.Split(" ");
            var roleB = claims[2].ToString();
            var roleBA = roleB.Split(" ");
            var date = claims[4].ToString();
            var p = date.Split(" ");
            return new Token()
            {
                email = emailA[1],
                authentication = bool.Parse(roleA[1]),
                admin = bool.Parse(roleBA[1]),
                expDate = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt32(p[1])).DateTime.ToLocalTime()
            };
        }
    }
}