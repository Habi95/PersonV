using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PersonData.model.person
{
    public class Token
    {
        public bool authentication { get; set; }
        public bool admin { get; set; }
        public DateTime expDate { get; set; }
    }
}