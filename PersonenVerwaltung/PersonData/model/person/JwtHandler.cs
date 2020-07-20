﻿using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

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
            var role = claims[0].ToString();
            var roleA = role.Split(" ");
            var date = claims[2].ToString();
            var p = date.Split(" ");
            return new Token()
            {
                authentication = bool.Parse(roleA[1]),
                expDate = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt32(p[1])).DateTime.ToLocalTime()
            };
        }
    }
}