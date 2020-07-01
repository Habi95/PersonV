
using PersonData.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonData
{
    public class Address : BaseAdress
    {
      
        public EContactType contact_type { get; set; }
        public bool billing_address { get; set; }

        public IList<AddressPerson> persons { get; set; }
    }
}
