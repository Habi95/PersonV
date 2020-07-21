using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityData.model
{
    public class Person : BasePerson
    {
        public int? user_id { get; set; }
        public List<Contact> contacts { get; set; }
        public User user { get; set; }
    }
}