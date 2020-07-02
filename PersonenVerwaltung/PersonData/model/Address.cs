using System;
using System.Collections.Generic;
using System.Text;

namespace PersonData
{
    public class Address : CreatedModify
    {
        public int id { get; set; }        
        public string street { get; set; }
        public string place { get; set; }
        public int zip { get; set; }
        public string country { get; set; }
        public EContactType contact_type { get; set; }
        public bool billing_address { get; set; }
    }
}
