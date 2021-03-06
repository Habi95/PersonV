﻿namespace PersonData
{
    public class Contact : BaseClassCreatedModify
    {
        public int person_id { get; set; }
        public EKindOfCommunication art_of_communication { get; set; }
        public string contact_value { get; set; }
        public EContactType contact_type { get; set; }
        public bool main_contact { get; set; }
        public Person person { get; set; }
    }
}