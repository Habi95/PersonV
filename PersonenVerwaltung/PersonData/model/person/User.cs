using System;
using System.Collections.Generic;
using System.Text;

namespace PersonData.model.person
{
    public class User : BaseClassCreatedModify
    {
        public string password { get; set; }
        public string security_word { get; set; }
        public bool authentication { get; set; }
        public Person person { get; set; }

        public User(string password, string security_word, bool authentication)
        {
            this.password = password;
            this.security_word = security_word;
            this.authentication = authentication;
        }
    }
}