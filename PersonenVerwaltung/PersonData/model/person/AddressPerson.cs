﻿namespace PersonData.model
{
    public class AddressPerson : BaseClassCreatedModify
    {
        public int addressId { get; set; }
        public int personId { get; set; }
        public EContactType contact_type { get; set; }
        public bool billing_address { get; set; }

        public Address address { get; set; }
        public Person person { get; set; }
    }
}