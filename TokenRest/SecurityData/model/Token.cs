using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityData.model
{
    public class Token
    {
        public string email { get; set; }
        public bool authentication { get; set; }
        public bool admin { get; set; }
        public DateTime expDate { get; set; }
    }
}